using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SkrinshoterApp.SkrApi;

public class QuotaApiClient
{
	private readonly IHttpClientFactory httpClientFactory;

	private readonly AppInfoProvider appInfoProvider;

	public QuotaApiClient(IHttpClientFactory httpClientFactory, AppInfoProvider appInfoProvider)
	{
		this.httpClientFactory = httpClientFactory;
		this.appInfoProvider = appInfoProvider;
	}

	public async Task<QuotaDto> GetQuota(CancellationToken cancellationToken)
	{
		using HttpClient httpClient = httpClientFactory.CreateClient("MainWithRetry");
		string content = JsonConvert.SerializeObject(new
		{
			appId = appInfoProvider.AppId
		});
		using StringContent content2 = new StringContent(content, Encoding.UTF8, "application/json");
		using HttpResponseMessage response = await httpClient.PostAsync("api/app/v1/quota", content2, cancellationToken);
		response.EnsureSuccessStatusCode();
		return JsonConvert.DeserializeObject<QuotaDto>(await response.Content.ReadAsStringAsync());
	}
}
