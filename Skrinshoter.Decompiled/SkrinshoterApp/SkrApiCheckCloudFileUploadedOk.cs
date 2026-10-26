using System.Net;
using System.Threading.Tasks;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp;

internal class SkrApiCheckCloudFileUploadedOk
{
	private string uID;

	public SkrApiCheckCloudFileUploadedOk(string uID)
	{
		this.uID = uID;
	}

	internal async Task<bool> StartAsync()
	{
		string requestUri = "/api/app/v1/check-file-uploaded-ok?fileId=" + uID;
		return (await SkrApiHttpClient.HttpClient.GetAsync(requestUri)).StatusCode == HttpStatusCode.OK;
	}
}
