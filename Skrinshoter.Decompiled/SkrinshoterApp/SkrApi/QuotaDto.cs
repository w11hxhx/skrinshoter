using Newtonsoft.Json;

namespace SkrinshoterApp.SkrApi;

public class QuotaDto
{
	[JsonProperty("used")]
	public long CloudStorageUsed { get; set; }

	[JsonProperty("limit")]
	public long CloudStorageTotal { get; set; }

	[JsonProperty("plan")]
	public string PlanId { get; set; }
}
