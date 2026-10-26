using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ScreenRecorderCommon;

namespace SkrinshoterApp.SkrChildApp;

public class VideoModuleAppProxy : ChildAppProxyBase
{
	public override char Delimiter => '|';

	public override string Name => "Video Module";

	public override string IncommingCommandPrefix => "event";

	public VideoModuleAppProxy()
		: base(new VideoModuleChildProcess(), new VideoModulePipeServer())
	{
	}

	protected override List<string> ExtractMessageFields(string message)
	{
		return message.Split(Delimiter).ToList();
	}

	public static async Task RestartVideoModuleAsync(IIPCProxy videoModule, int retriesCounter)
	{
		int num = retriesCounter % 3;
		Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\DirectX\\UserGpuPreferences", videoModule.Process.ProcessPath, $"GpuPreference={num};");
		AppLogger.Logger.Info($"Set GpuPreference={num} for \"{videoModule.Process.ProcessPath}\"");
		await videoModule.SendCommand(VideoModuleCommand.terminate);
		string[] array = await videoModule.ReceiveCommandResult(VideoModuleCommand.terminate, TimeSpan.FromSeconds(5.0));
		if (array == null || array.Length == 0)
		{
			throw new Exception("Invalid response from video module for command 'terminate'");
		}
		if (array[0] == "error")
		{
			throw new Exception("Video module termination has been failed with code " + array[1] + ": " + array[2]);
		}
		await videoModule.WaitForProcessDisconnected(TimeSpan.FromSeconds(5.0));
		await videoModule.Process.WaitForProcessExited(TimeSpan.FromSeconds(5.0));
	}
}
