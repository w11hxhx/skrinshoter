using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SkrinshoterApp;

public sealed class ForkRelease
{
    public const string Repository = "w11hxhx/skrinshoter";
    public const string InstallerName = "Skrinshoter-setup-win-x64.exe";
    public Version Version { get; private set; }
    public string Notes { get; private set; }
    public Uri InstallerUrl { get; private set; }
    public string Sha256 { get; private set; }
    public long Size { get; private set; }

    public static ForkRelease Parse(string json)
    {
        var data = JObject.Parse(json);
        if ((bool?)data["draft"] == true || (bool?)data["prerelease"] == true)
            throw new InvalidDataException("Предварительные релизы не устанавливаются автоматически.");
        string tag = (string)data["tag_name"];
        if (tag == null || !Regex.IsMatch(tag, @"^v?\d+\.\d+\.\d+(\.\d+)?$") ||
            !System.Version.TryParse(tag.TrimStart('v'), out var version))
            throw new InvalidDataException("Некорректная версия релиза форка.");
        var asset = data["assets"]?.FirstOrDefault(x => (string)x["name"] == InstallerName);
        string expectedUrl = "https://github.com/" + Repository + "/releases/download/" + tag + "/" + InstallerName;
        if (asset == null || (string)asset["state"] != "uploaded" ||
            (string)asset["browser_download_url"] != expectedUrl || ((long?)asset["size"] ?? 0) <= 0)
            throw new InvalidDataException("В релизе пока нет готового установщика форка.");
        string digest = (string)asset["digest"];
        if (digest == null || !Regex.IsMatch(digest, "^sha256:[a-fA-F0-9]{64}$"))
            throw new InvalidDataException("GitHub не предоставил контрольную сумму установщика.");
        return new ForkRelease {
            Version = version, Notes = (string)data["body"] ?? "Описание изменений отсутствует.",
            InstallerUrl = new Uri(expectedUrl), Sha256 = digest.Substring(7), Size = (long)asset["size"]
        };
    }

    public bool ShouldOffer(Version current, string skippedVersion) =>
        Version > current && !string.Equals(Version.ToString(), skippedVersion, StringComparison.Ordinal);
}

public sealed class ForkUpdateService
{
    private readonly IHttpClientFactory clients;
    public ForkUpdateService(IHttpClientFactory clients) { this.clients = clients; }

    public async Task<ForkRelease> GetLatestAsync(CancellationToken token)
    {
        using var client = clients.CreateClient(HttpClientConfig.Names.AppUpdateChecker);
        using var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.github.com/repos/" + ForkRelease.Repository + "/releases/latest");
        using var response = await client.SendAsync(request, token).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        token.ThrowIfCancellationRequested();
        return ForkRelease.Parse(json);
    }

    public async Task<string> DownloadAsync(ForkRelease release, IProgress<int> progress, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
        timeout.CancelAfter(TimeSpan.FromMinutes(10));
        token = timeout.Token;
        string directory = Path.Combine(Path.GetTempPath(), "Skrinshoter-fork-update-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        string path = Path.Combine(directory, ForkRelease.InstallerName);
        try
        {
            using var client = clients.CreateClient(HttpClientConfig.Names.ForkUpdateDownload);
            using var response = await client.GetAsync(release.InstallerUrl, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using (var source = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var target = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true))
            using (var sha = SHA256.Create())
            {
                byte[] buffer = new byte[81920];
                long received = 0;
                int count;
                while ((count = await source.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false)) > 0)
                {
                    received += count;
                    if (received > release.Size) throw new InvalidDataException("Размер установщика отличается от опубликованного.");
                    sha.TransformBlock(buffer, 0, count, null, 0);
                    await target.WriteAsync(buffer, 0, count, token).ConfigureAwait(false);
                    progress?.Report((int)(received * 100 / release.Size));
                }
                sha.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
                string actual = BitConverter.ToString(sha.Hash).Replace("-", "");
                if (received != release.Size || !string.Equals(actual, release.Sha256, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidDataException("Проверка SHA-256 не пройдена. Установщик не будет запущен.");
            }
            token.ThrowIfCancellationRequested();
            return path;
        }
        catch
        {
            // Only the newly created update file/directory are owned by this operation.
            try { File.Delete(path); Directory.Delete(directory); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            throw;
        }
    }
}
