using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp;

internal class SkrApiFileUpload
{
	private struct ResponseBody
	{
		[JsonProperty("status")]
		public int Status { get; set; }
	}

	private string name;

	private string path;

	private string uID;

	private int type;

	private string ext;

	private string windowTitle;

	private bool isScrollingCapture;

	private string appId;

	public string FileUidWithNoDate => uID.Split('/')[1];

	public string FileSourcePath => path;

	public string FileName => name + ext;

	public event EventHandler<int> OnProgress;

	public SkrApiFileUpload(string appId, string name, string path, string uID, int type, string url, string ext, string windowTitle, bool isScrollingCapture)
	{
		this.appId = appId;
		this.name = name;
		this.path = path;
		this.uID = uID;
		this.type = type;
		this.ext = ext;
		this.windowTitle = windowTitle;
		this.isScrollingCapture = isScrollingCapture;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		string text = name;
		if (AppInfoProvider.IsFloomby)
		{
			text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
		}
		Dictionary<string, string> additionalFields = new Dictionary<string, string>
		{
			{ "appId", appId },
			{ "fileName", text },
			{ "screenshotId", uID },
			{
				"uploadType",
				type.ToString()
			},
			{ "fileExt", ext },
			{ "windowTitle", windowTitle },
			{
				"scrollingCapture",
				isScrollingCapture ? "1" : "0"
			}
		};
		await new FilePostUploader(SkrApiHttpClient.HttpClient, "userfile", path, additionalFields, "/post_file2", cancellationToken, (int progress) =>
		{
			OnProgress?.Invoke(this, progress);
		}, async (HttpResponseMessage repsponse) =>
		{
			ResponseBody responseBody = JsonConvert.DeserializeObject<ResponseBody>(await repsponse.Content.ReadAsStringAsync());
			if (responseBody.Status != 0)
			{
				throw new Exception($"Response status is not 0. {responseBody.Status}");
			}
		}).Upload();
	}
}
