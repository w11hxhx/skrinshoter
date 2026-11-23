using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SkrinshoterApp;
using Microsoft.Extensions.Logging.Abstractions;

internal static class ForkUpdateTests
{
    private static void Check(bool value, string error) { if (!value) throw new Exception(error); }
    public static async Task RunAsync()
    {
        byte[] payload = Encoding.UTF8.GetBytes("test installer payload; never executed");
        string hash;
        using (var sha = SHA256.Create()) hash = BitConverter.ToString(sha.ComputeHash(payload)).Replace("-", "").ToLowerInvariant();
        var data = new JObject {
            ["tag_name"] = "v4.87.4.99", ["draft"] = false, ["prerelease"] = false,
            ["body"] = "Добавили обновления\nИсправили ошибки\n<TextBlock />",
            ["assets"] = new JArray(new JObject {
                ["name"] = ForkRelease.InstallerName, ["state"] = "uploaded", ["size"] = payload.Length,
                ["digest"] = "sha256:" + hash,
                ["browser_download_url"] = "https://github.com/w11hxhx/skrinshoter/releases/download/v4.87.4.99/" + ForkRelease.InstallerName
            })
        };
        var release = ForkRelease.Parse(data.ToString());
        Check(release.Notes == (string)data["body"], "Release notes changed or were interpreted as markup.");
        Check(release.ShouldOffer(new Version("4.87.4.7"), ""), "New fork release was hidden.");
        Check(!release.ShouldOffer(release.Version, "") && !release.ShouldOffer(new Version("5.0.0.0"), ""), "Updater offered same version or downgrade.");
        Check(!release.ShouldOffer(new Version("4.87.4.7"), "4.87.4.99"), "Skipped release was offered.");
        Reject(data, x => x["prerelease"] = true);
        Reject(data, x => x["draft"] = true);
        Reject(data, x => x["tag_name"] = "v4.87.4.99/../../other");
        Reject(data, x => x["assets"][0]["browser_download_url"] = "https://skrinshoter.ru/SkrinshoterSetup.exe");
        Reject(data, x => x["assets"][0]["digest"] = null);
        Reject(data, x => ((JObject)x["assets"][0]).Remove("size"));
        Reject(data, x => x["assets"][0]["state"] = "new");
        var factory = new Factory(data.ToString(), payload);
        var updates = new ForkUpdateService(factory);
        Check((await updates.GetLatestAsync(CancellationToken.None)).Version == release.Version, "GitHub release response not parsed.");
        Check(factory.RequestedApi, "Updater did not request the fork's GitHub endpoint.");
        string path = await updates.DownloadAsync(release, null, CancellationToken.None);
        try { Check(Convert.ToBase64String(File.ReadAllBytes(path)) == Convert.ToBase64String(payload), "Verified download differs from payload."); }
        finally { File.Delete(path); Directory.Delete(Path.GetDirectoryName(path)); }
        factory.Corrupt = true;
        try { await updates.DownloadAsync(release, null, CancellationToken.None); throw new Exception("Corrupted update accepted."); }
        catch (InvalidDataException) { }
        using (var cts = new CancellationTokenSource())
        {
            factory.Corrupt = false;
            factory.WaitForCancellation = true;
            var pending = updates.DownloadAsync(release, null, cts.Token);
            cts.Cancel();
            try { await pending; throw new Exception("Cancelled update accepted."); }
            catch (OperationCanceledException) { }
        }
        factory.WaitForCancellation = false;
        var settings = new SettingsManager();
        var shutdown = new AppShutdownService();
        var operations = new OperationStateService();
        var installer = new Installer();
        using (var vm = new AppUpdateCheckWindowVM(new AppInfoProvider(), updates, settings, shutdown, operations,
            installer, NullLogger<AppUpdateCheckWindowVM>.Instance))
        {
            Check(!vm.DownloadNewVersionCommand.CanExecute(null), "Installer enabled before version check.");
            vm.Initialize(release);
            await vm.downloadUpdatesTask;
            Check(installer.Calls == 0 && shutdown.Calls == 0 && vm.ReleaseNotes == release.Notes, "Opening release notes installed or closed the app.");
            vm.SkipVersionCommand.Execute(null);
            Check(settings.Settings.SkippedForkUpdateVersion == release.Version.ToString(), "Skipped version not saved.");
            vm.CheckForUpdates = false;
            Check(!settings.Settings.CheckForUpdates, "Automatic fork updates could not be disabled.");
            operations.TryStartOperation(OperationType.Screenshot);
            await vm.DownloadNewVersionCommand.ExecuteAsync(null);
            Check(installer.Calls == 0, "Installer interrupted an active capture.");
            operations.StopOperation();
            factory.Corrupt = true;
            await vm.DownloadNewVersionCommand.ExecuteAsync(null);
            Check(installer.Calls == 0 && shutdown.Calls == 0, "Corrupt download reached installer.");
            factory.Corrupt = false;
            factory.WaitForCancellation = true;
            var pending = vm.DownloadNewVersionCommand.ExecuteAsync(null);
            vm.CancelDownloadCommand.Execute(null);
            await pending;
            Check(installer.Calls == 0 && shutdown.Calls == 0 && !vm.Busy, "Cancelled update installed or left the app blocked.");
            factory.WaitForCancellation = false;
            await vm.DownloadNewVersionCommand.ExecuteAsync(null);
            Check(installer.Calls == 1 && shutdown.Calls == 1, "Explicit consent did not launch verified installer exactly once.");
            File.Delete(installer.Path);
            Directory.Delete(System.IO.Path.GetDirectoryName(installer.Path));
        }
        Console.WriteLine("PASS: update consent, notes, disabling, skipping, cancellation and installer handoff.");
        Console.WriteLine("PASS: fork release source, notes, skipped versions, no downgrade, verified downloads and cancellation.");
    }
    private static void Reject(JObject original, Action<JObject> change)
    {
        var data = (JObject)original.DeepClone(); change(data);
        try { ForkRelease.Parse(data.ToString()); throw new Exception("Invalid update metadata accepted."); }
        catch (InvalidDataException) { }
    }
    private sealed class Installer : IForkUpdateInstaller
    {
        public int Calls;
        public string Path;
        public void Start(string installerPath, string directory)
        {
            Check(File.Exists(installerPath) && directory == AppDomain.CurrentDomain.BaseDirectory.TrimEnd(System.IO.Path.DirectorySeparatorChar), "Installer handoff lost verified file or target directory.");
            Calls++; Path = installerPath;
        }
    }
    private sealed class Factory : HttpMessageHandler, IHttpClientFactory
    {
        private readonly string json;
        private readonly byte[] payload;
        public bool RequestedApi, Corrupt, WaitForCancellation;
        public Factory(string json, byte[] payload) { this.json = json; this.payload = payload; }
        public HttpClient CreateClient(string name) => new HttpClient(this, false);
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (request.RequestUri.Host == "api.github.com")
            {
                Check(request.RequestUri.AbsoluteUri == "https://api.github.com/repos/w11hxhx/skrinshoter/releases/latest", "Wrong update API.");
                RequestedApi = true;
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json) };
            }
            Check(request.RequestUri.Host == "github.com" && request.RequestUri.AbsolutePath.StartsWith("/w11hxhx/skrinshoter/releases/download/"), "Download left fork repository.");
            if (WaitForCancellation) await Task.Delay(Timeout.Infinite, token);
            var bytes = (byte[])payload.Clone();
            if (Corrupt) bytes[0] ^= 1;
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(bytes) };
        }
    }
}
