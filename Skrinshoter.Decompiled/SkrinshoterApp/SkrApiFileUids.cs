using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp;

internal class SkrApiFileUids
{
	private readonly HttpClient httpClient;

	public SkrApiFileUids(HttpClient httpClient = null)
	{
		this.httpClient = httpClient ?? SkrApiHttpClient.HttpClient;
	}

	public class ApiResponseFileUids
	{
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("uids")]
		public List<string> Uids { get; set; }
	}

	internal async Task<IEnumerable<string>> GetFileUids()
	{
		AppLogger.Logger.Info("SkrAPI:GetFileUids");
		string text = await httpClient.GetStringAsync("/api/app/v1/uids.php");
		ApiResponseFileUids? apiResponseFileUids = JsonConvert.DeserializeObject<ApiResponseFileUids>(text);
		AppLogger.Logger.Info("SkrAPI:GetFileUids: got " + text);
		return apiResponseFileUids.Uids;
	}
}
