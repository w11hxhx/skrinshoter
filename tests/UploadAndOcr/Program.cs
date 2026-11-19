using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging.Abstractions;
using SkrinshoterApp;
using SkrinshoterApp.Services.Progress;

internal static class Program
{
    private static void Check(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
    }

    private static async Task Main()
    {
        string folder = Path.Combine(Path.GetTempPath(), "skrinshoter-tests-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);
        try
        {
            string file = Path.Combine(folder, "image.png");
            File.WriteAllBytes(file, new byte[200000]);
            var operations = new OperationStateService();
            Check(operations.TryStartOperation(OperationType.Screenshot), "First capture was blocked.");
            long first = operations.OperationId;
            operations.StopOperation();
            Check(operations.TryStartOperation(OperationType.Screenshot), "Background upload blocked a new capture.");
            operations.StopOperation(first);
            Check(operations.CurrentOperation == OperationType.Screenshot, "Old upload released a newer capture lock.");
            operations.StopOperation(operations.OperationId);
            Check(operations.CurrentOperation == OperationType.None, "Current capture lock was not released.");
            await CheckUploads(file);
            await CheckOcr(folder);
            Console.WriteLine("PASS: concurrent uploads, stable links, partial failures, cancellation, OCR and invalid images.");
        }
        finally { Directory.Delete(folder, true); }
    }

    private static async Task CheckUploads(string file)
    {
        var handler = new GateHandler();
        var sessions = new Sessions();
        var settings = new SettingsManager();
        settings.Settings.CustomUploadProviders.Add(new CustomUploadProvider { Name = "First", Url = "https://test.invalid/first" });
        settings.Settings.CustomUploadProviders.Add(new CustomUploadProvider { Name = "Second", Url = "https://test.invalid/second" });
        var notifications = new NotificationService();
        var cloud = new CloudFileUploadService();
        var service = new ExternalScreenshotUploadService(new ClientFactory(handler), sessions);
        var coordinator = new ScreenshotUploadCoordinator(cloud, service, notifications,
            NullLogger<ScreenshotUploadCoordinator>.Instance, settings);
        var upload = coordinator.UploadAsync(file, "sample", "title", false,
            ScreenshotUploadDestination.Custom | ScreenshotUploadDestination.Skrinshoter);
        await WithTimeout(handler.BothStarted.Task);
        Check(!upload.IsCompleted, "Upload completed before the responses arrived.");
        handler.Second.TrySetResult(true);
        await WithTimeout(handler.SecondFinished.Task);
        Check(!upload.IsCompleted, "Temporary file could be deleted while the first upload is still reading.");
        handler.First.TrySetResult(true);
        var results = await WithTimeout(upload);
        Check(results.Count == 3 && results[0].ServiceName == "First" && results[1].ServiceName == "Second", "Links lost provider order.");
        Check(cloud.Links.Contains("https://test.invalid/first.png") && cloud.Links.Contains("https://test.invalid/second.png"), "Cloud title lost external links.");
        Check(handler.Payloads == 2 && sessions.Disposed == 2, "Upload streams or progress sessions were not finished.");

        handler = new GateHandler { FailFirst = true };
        service = new ExternalScreenshotUploadService(new ClientFactory(handler), sessions);
        coordinator = new ScreenshotUploadCoordinator(cloud, service, notifications, NullLogger<ScreenshotUploadCoordinator>.Instance, settings);
        upload = coordinator.UploadAsync(file, "sample", "", false, ScreenshotUploadDestination.Custom);
        await WithTimeout(handler.BothStarted.Task);
        handler.First.TrySetResult(true);
        handler.Second.TrySetResult(true);
        results = await WithTimeout(upload);
        Check(results.Count == 1 && results[0].ServiceName == "Second" && notifications.Errors == 1, "Partial failure discarded a successful link.");

        handler = new GateHandler { CancelFirst = true };
        service = new ExternalScreenshotUploadService(new ClientFactory(handler), sessions);
        coordinator = new ScreenshotUploadCoordinator(cloud, service, notifications, NullLogger<ScreenshotUploadCoordinator>.Instance, settings);
        upload = coordinator.UploadAsync(file, "sample", "", false, ScreenshotUploadDestination.Custom);
        await WithTimeout(handler.BothStarted.Task);
        handler.First.TrySetResult(true);
        handler.Second.TrySetResult(true);
        results = await WithTimeout(upload);
        Check(results.Count == 1 && results[0].ServiceName == "Second", "Cancelling one service discarded the other link.");

        handler = new GateHandler();
        service = new ExternalScreenshotUploadService(new ClientFactory(handler), sessions);
        coordinator = new ScreenshotUploadCoordinator(cloud, service, notifications, NullLogger<ScreenshotUploadCoordinator>.Instance, settings);
        using (var cancellation = new CancellationTokenSource())
        {
            upload = coordinator.UploadAsync(file, "sample", "", false, ScreenshotUploadDestination.Custom, cancellation.Token);
            await WithTimeout(handler.BothStarted.Task);
            cancellation.Cancel();
            try { await WithTimeout(upload); throw new Exception("Cancellation was ignored."); }
            catch (OperationCanceledException) { }
            Check(handler.Active == 0, "Cancellation returned before all requests stopped.");
        }
        using (File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
    }

    private static async Task CheckOcr(string folder)
    {
        string file = Path.Combine(folder, "text sample тест.png");
        using (var bitmap = new Bitmap(1000, 220))
        using (var graphics = Graphics.FromImage(bitmap))
        using (var font = new Font("Arial", 32))
        {
            graphics.Clear(Color.White);
            graphics.DrawString("SCREENSHOT 123\nHELLO WORLD", font, Brushes.Black, 20, 20);
            bitmap.Save(file, System.Drawing.Imaging.ImageFormat.Png);
        }
        string text;
        try { text = await WithTimeout(WindowsTextRecognition.RecognizeAsync(file), 65000); }
        catch (InvalidOperationException e) when (e.Message.Contains("Install an OCR language"))
        {
            Console.WriteLine("SKIP: OCR recognition requires a Windows OCR language pack.");
            return;
        }
        Check(text.Contains("123") && text.IndexOf("HELLO", StringComparison.OrdinalIgnoreCase) >= 0 && text.Contains(Environment.NewLine), "OCR lost text, Unicode paths or line breaks: " + text);
        // Force the exact preferred-language mismatch that used to stop recognition.
        string script = (string)typeof(WindowsTextRecognition).GetField("Script", BindingFlags.Static | BindingFlags.NonPublic).GetRawConstantValue();
        script = script.Replace("$engine = [Windows.Media.Ocr.OcrEngine]::TryCreateFromUserProfileLanguages()", "$engine = $null");
        var start = new ProcessStartInfo
        {
            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"WindowsPowerShell\v1.0\powershell.exe"),
            Arguments = "-NoProfile -NonInteractive -EncodedCommand " + Convert.ToBase64String(Encoding.Unicode.GetBytes(script)),
            UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true
        };
        start.EnvironmentVariables["SKR_OCR_IMAGE"] = file;
        using (var process = Process.Start(start))
        {
            var output = process.StandardOutput.ReadToEndAsync();
            var error = process.StandardError.ReadToEndAsync();
            if (!process.WaitForExit(60000)) { process.Kill(); throw new TimeoutException("Fallback OCR timed out."); }
            Check(process.ExitCode == 0, "Installed-language fallback failed: " + await error);
            Check(Encoding.UTF8.GetString(Convert.FromBase64String((await output).Trim())).Contains("123"), "Fallback OCR lost the sample text.");
        }
        using (var cancellation = new CancellationTokenSource())
        {
            cancellation.CancelAfter(10);
            try { await WindowsTextRecognition.RecognizeAsync(file, cancellation.Token); throw new Exception("Running OCR cancellation ignored."); }
            catch (OperationCanceledException) { }
        }
        using (File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
        using (var bitmap = new Bitmap(200, 100))
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.White);
            bitmap.Save(file, System.Drawing.Imaging.ImageFormat.Png);
        }
        Check(string.IsNullOrWhiteSpace(await WithTimeout(WindowsTextRecognition.RecognizeAsync(file), 65000)), "Blank image returned text.");
        File.WriteAllText(file, "not an image");
        try { await WithTimeout(WindowsTextRecognition.RecognizeAsync(file), 65000); throw new Exception("Invalid image accepted."); }
        catch (InvalidOperationException) { }
        using (var cancellation = new CancellationTokenSource())
        {
            cancellation.Cancel();
            try { await WindowsTextRecognition.RecognizeAsync(file, cancellation.Token); throw new Exception("OCR cancellation ignored."); }
            catch (OperationCanceledException) { }
        }
        try { await WindowsTextRecognition.RecognizeAsync(Path.Combine(folder, "missing.png")); throw new Exception("Missing image accepted."); }
        catch (FileNotFoundException) { }
    }

    private static async Task WithTimeout(Task task, int milliseconds = 10000)
    {
        if (await Task.WhenAny(task, Task.Delay(milliseconds)) != task) throw new TimeoutException("Test timed out.");
        await task;
    }
    private static async Task<T> WithTimeout<T>(Task<T> task, int milliseconds = 10000)
    {
        await WithTimeout((Task)task, milliseconds);
        return await task;
    }

    private sealed class GateHandler : HttpMessageHandler
    {
        public readonly TaskCompletionSource<bool> BothStarted = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        public readonly TaskCompletionSource<bool> First = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        public readonly TaskCompletionSource<bool> Second = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        public readonly TaskCompletionSource<bool> SecondFinished = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        public int Active, Payloads;
        public bool FailFirst, CancelFirst;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            bool first = request.RequestUri.AbsolutePath == "/first";
            Interlocked.Increment(ref Active);
            try
            {
                using (var output = new MemoryStream())
                {
                    await request.Content.CopyToAsync(output);
                    Check(output.Length > 200000, "Multipart body missing file content.");
                    Interlocked.Increment(ref Payloads);
                }
                if (Payloads == 2) BothStarted.TrySetResult(true);
                using (token.Register(() => (first ? First : Second).TrySetCanceled()))
                    await (first ? First.Task : Second.Task);
                if (first && CancelFirst) throw new OperationCanceledException();
                if (!first) SecondFinished.TrySetResult(true);
                return new HttpResponseMessage(first && FailFirst ? HttpStatusCode.InternalServerError : HttpStatusCode.OK)
                {
                    Content = new StringContent("https://test.invalid/" + (first ? "first" : "second") + ".png")
                };
            }
            finally { Interlocked.Decrement(ref Active); }
        }
    }
    private sealed class ClientFactory : IHttpClientFactory
    {
        private readonly HttpMessageHandler handler;
        public ClientFactory(HttpMessageHandler handler) { this.handler = handler; }
        public HttpClient CreateClient(string name) => new HttpClient(handler, false);
    }
    private sealed class Sessions : IProgressWindowService
    {
        public int Disposed;
        public IProgressSession TryShow(string message, bool showUi, Window owner = null, CancellationToken externalToken = default) => new Session(this, externalToken);
        public IProgressSession TryShow(string message, bool showUi, IntPtr ownerHwnd, CancellationToken externalToken = default) => new Session(this, externalToken);
        private sealed class Session : IProgressSession, IProgress<int>
        {
            private readonly Sessions parent;
            public Session(Sessions parent, CancellationToken token) { this.parent = parent; Token = token; }
            public IProgress<int> Progress => this;
            public CancellationToken Token { get; }
            public void Report(int value) { Check(value >= 0 && value <= 100, "Invalid progress percentage."); }
            public void SetIndeterminate(bool value) { }
            public void UpdateMessage(string message) { }
            public void Dispose() { Interlocked.Increment(ref parent.Disposed); }
        }
    }
}

// Infrastructure doubles: the coordinator, HTTP multipart uploader, stream and OCR are production sources.
namespace SkrinshoterApp
{
    public sealed class SettingsManager { public Settings Settings { get; } = new Settings(); }
    public sealed class Settings { public List<CustomUploadProvider> CustomUploadProviders { get; } = new List<CustomUploadProvider>(); }
    public sealed class NotificationService { public int Errors; public void ShowError(string text) { Errors++; } }
    public enum CloudFileUploadType { Screenshot }
    public sealed class CloudFileUploadService
    {
        public string Links;
        public Task<string> UploadFile(string path, CloudFileUploadType type, string name, string extension, string title,
            bool scrolling, bool copyLinkToClipboard) { Links = title; return Task.FromResult("https://test.invalid/cloud.png"); }
    }
    public static class HttpClientConfig { public static class Names { public const string ExternalScreenshotUpload = "test"; } }
}
namespace System.Net.Http
{
    public interface IHttpClientFactory { HttpClient CreateClient(string name); }
}
