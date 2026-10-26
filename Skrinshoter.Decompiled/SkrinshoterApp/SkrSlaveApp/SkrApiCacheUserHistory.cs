using System.Threading.Tasks;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp.SkrSlaveApp;

internal class SkrApiCacheUserHistory
{
	private string _appId;

	public SkrApiCacheUserHistory(string appId)
	{
		_appId = appId;
	}

	public async Task Execute()
	{
		string appId = _appId;
		string requestUri = "/history_add_to_cache.php?appid=" + appId;
		(await SkrApiHttpClient.HttpClient.GetAsync(requestUri)).EnsureSuccessStatusCode();
	}
}
