using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SkrinshoterApp;

internal static class GoogleLensBrowserSearch
{
    internal static async Task OpenAsync(string imagePath)
    {
        string directory = Path.Combine(Path.GetTempPath(), "Skrinshoter-Lens");
        Directory.CreateDirectory(directory);
        foreach (string oldPage in Directory.EnumerateFiles(directory, "*.html"))
        {
            if (Guid.TryParseExact(Path.GetFileNameWithoutExtension(oldPage), "N", out _) &&
                File.GetLastWriteTimeUtc(oldPage) < DateTime.UtcNow.AddMinutes(-10))
                DeletePage(oldPage);
        }

        string image = await Task.Run(() => Convert.ToBase64String(File.ReadAllBytes(imagePath)));
        string page = Path.Combine(directory, Guid.NewGuid().ToString("N") + ".html");
        const string template = """
            <!doctype html>
            <html lang="ru">
            <meta charset="utf-8">
            <title>Google Lens</title>
            <p>Отправка снимка в Google Lens…</p>
            <form id="lens" action="https://lens.google.com/upload" method="post" enctype="multipart/form-data">
              <input id="image" name="encoded_image" type="file" accept="image/png" required>
              <button>Отправить в Google Lens</button>
            </form>
            <script>
              const bytes = Uint8Array.from(atob("SCREENSHOT_BASE64"), character => character.charCodeAt(0));
              const selection = new DataTransfer();
              selection.items.add(new File([bytes], "screenshot.png", { type: "image/png" }));
              document.getElementById("image").files = selection.files;
              document.getElementById("lens").requestSubmit();
            </script>
            </html>
            """;
        try
        {
            File.WriteAllText(page, template.Replace("SCREENSHOT_BASE64", image));
            using var process = Process.Start(new ProcessStartInfo(page) { UseShellExecute = true });
        }
        catch
        {
            DeletePage(page);
            throw;
        }
        _ = DeleteLaterAsync(page);
    }

    private static async Task DeleteLaterAsync(string page)
    {
        await Task.Delay(TimeSpan.FromMinutes(10)).ConfigureAwait(false);
        DeletePage(page);
    }

    private static void DeletePage(string page)
    {
        try { File.Delete(page); }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }
}
