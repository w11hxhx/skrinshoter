using System;

namespace SkrinshoterApp;

public class CloudStorageQuotaExceededException : Exception
{
	public CloudStorageQuotaExceededException(Exception inner = null)
		: base("Cloud storage quota exceeded.", inner)
	{
	}
}
