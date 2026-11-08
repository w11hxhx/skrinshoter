using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp;

internal class SkrApiCheckCloudFileUploadedOk
{
	private string uID;

	private readonly HttpClient httpClient;

	public SkrApiCheckCloudFileUploadedOk(string uID, HttpClient httpClient = null)
	{
		this.uID = uID;
		this.httpClient = httpClient ?? SkrApiHttpClient.HttpClient;
	}

	internal async Task<bool> StartAsync()
	{
		string requestUri = "/api/app/v1/check-file-uploaded-ok?fileId=" + uID;
		return (await httpClient.GetAsync(requestUri)).StatusCode == HttpStatusCode.OK;
	}
}
