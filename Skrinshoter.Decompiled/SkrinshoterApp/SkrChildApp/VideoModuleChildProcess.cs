using System.IO;
using System.Reflection;

namespace SkrinshoterApp.SkrChildApp;

internal class VideoModuleChildProcess : ChildProcessBase, IChildProcess
{
	public override string ProcessName => "scrrec.exe";

	public override string ProcessPath => Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "scrrec"), ProcessName);
}
