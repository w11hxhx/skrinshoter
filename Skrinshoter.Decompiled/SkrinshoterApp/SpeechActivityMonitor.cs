using System;
using System.Diagnostics;

namespace SkrinshoterApp;

[AnyThread]
internal sealed class SpeechActivityMonitor
{
	private const int LingerSeconds = 3;

	private const int MinHiddenSeconds = 1;

	private readonly object syncRoot = new object();

	private bool speechActive;

	private long speechActiveChangeTimestamp;

	private bool warningDesired;

	private long warningHiddenTimestamp;

	public bool WarningDesired
	{
		get
		{
			lock (syncRoot)
			{
				return warningDesired;
			}
		}
	}

	public event EventHandler WarningDesiredChanged;

	public void Report(bool isActive)
	{
		bool flag2;
		lock (syncRoot)
		{
			long timestamp = Stopwatch.GetTimestamp();
			if (isActive != speechActive)
			{
				speechActive = isActive;
				speechActiveChangeTimestamp = timestamp;
			}
			bool flag = warningDesired;
			if (warningDesired)
			{
				if (!speechActive && timestamp - speechActiveChangeTimestamp >= 3 * Stopwatch.Frequency)
				{
					flag = false;
				}
			}
			else if (speechActive && timestamp - warningHiddenTimestamp >= Stopwatch.Frequency)
			{
				flag = true;
			}
			flag2 = flag != warningDesired;
			if (flag2)
			{
				warningDesired = flag;
				if (!flag)
				{
					warningHiddenTimestamp = timestamp;
				}
			}
		}
		if (flag2)
		{
			WarningDesiredChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public void Reset()
	{
		lock (syncRoot)
		{
			speechActive = false;
			speechActiveChangeTimestamp = 0L;
			warningDesired = false;
			warningHiddenTimestamp = Stopwatch.GetTimestamp();
		}
	}
}
