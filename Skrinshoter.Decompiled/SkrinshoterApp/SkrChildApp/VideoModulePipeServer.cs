using System.Security.Principal;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.SkrChildApp;

internal class VideoModulePipeServer : SkrPipeServerBase
{
	public override string GetPipeName()
	{
		return $"{ProductResources.AppInvariantName}VideoModulePipe_{WindowsIdentity.GetCurrent().User}";
	}
}
