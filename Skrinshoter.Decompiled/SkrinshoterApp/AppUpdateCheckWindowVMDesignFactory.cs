using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp;

public sealed class AppUpdateCheckWindowVMDesignFactory : DesignFactory<AppUpdateCheckWindowVM>
{
	public override AppUpdateCheckWindowVM Create(IServiceProvider sp)
	{
		AppUpdateCheckWindowVM vm = ActivatorUtilities.CreateInstance<AppUpdateCheckWindowVM>(sp, Array.Empty<object>());
		vm.CurrentVersion = "0.0";
		vm.cts.Cancel();
		vm.downloadUpdatesTask = Task.Run(() =>
		{
			string uri = (AppInfoProvider.IsFloomby ? "SkrinshoterApp/VersionHistory/Floomby/version_history.en.xml" : "SkrinshoterApp/VersionHistory/Skrinshoter/version_history.xml");
			vm.SetFlowDocument(XDocument.Load(uri));
			vm.State = AppUpdateCheckWindowState.Loaded;
			return Task.CompletedTask;
		});
		return vm;
	}
}
