using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public sealed class ProgressWindowVMDesignFactory : DesignFactory<ProgressWindowVM>
{
	public override ProgressWindowVM Create(IServiceProvider sp)
	{
		ProgressWindowVM progressWindowVM = ActivatorUtilities.CreateInstance<ProgressWindowVM>(sp, new object[2]
		{
			new CancellationTokenSource(),
			Resources.Progress_LinkToFileAlreadyInClipboard
		});
		progressWindowVM.Progress = 70;
		return progressWindowVM;
	}
}
