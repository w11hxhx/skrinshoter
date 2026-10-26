using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Win32;

namespace SkrinshoterApp;

public class DiskSpaceMonitor
{
	public string DiskName { get; }

	public DiskSpaceMonitor(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		DiskName = Path.GetPathRoot(path);
	}

	public unsafe ulong GetFreeSpace()
	{
		ulong result = default;
		if (!PInvoke.GetDiskFreeSpaceEx(DiskName, &result, null, null))
		{
			throw new InvalidOperationException("GetDiskFreeSpaceEx(\"" + DiskName + "\") failed.", new Win32Exception());
		}
		return result;
	}

	public async Task WaitForLowDiskSpaceAsync(ulong diskSpaceThreshold, CancellationToken cancellationToken = default(CancellationToken))
	{
		while (GetFreeSpace() > diskSpaceThreshold)
		{
			await Task.Delay(100, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
	}
}
