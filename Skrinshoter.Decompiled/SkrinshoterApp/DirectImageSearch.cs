using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SkrinshoterApp;

internal static class DirectImageSearch
{
    private const string YandexRequest = "{\"blocks\":[{\"block\":\"b-page_type_search-by-image__link\"}]}";

    internal static async Task<Uri> UploadToYandexAsync(string path)
    {
        using var handler = new HttpClientHandler { AllowAutoRedirect = false, UseCookies = false };
        using var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(30) };
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Skrinshoter/1.0");
        var endpoint = "https://yandex.ru/images/search?rpt=imageview&format=json&request=" + Uri.EscapeDataString(YandexRequest);
        using var stream = File.OpenRead(path);
        using var form = new MultipartFormDataContent();
        using var image = new StreamContent(stream);
        image.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        form.Add(image, "upfile", "screenshot.png");
        using var response = await client.PostAsync(endpoint, form).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var json = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        var cbirId = (string)json.SelectToken("blocks[0].params.cbirId");
        if (cbirId == null || !Regex.IsMatch(cbirId, @"\A[0-9]+/[A-Za-z0-9_-]+\z"))
            throw new InvalidDataException("Яндекс Картинки не вернули идентификатор поиска.");
        return new Uri("https://yandex.ru/images/search?rpt=imageview&cbir_id=" + Uri.EscapeDataString(cbirId));
    }
}
