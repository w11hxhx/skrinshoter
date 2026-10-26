using System;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp.BackgroundServices;

public sealed class UserQuotaUpdatedEventArgs : EventArgs
{
	public QuotaDto Quota { get; }

	public UserQuotaUpdatedEventArgs(QuotaDto quota)
	{
		Quota = quota;
	}
}
