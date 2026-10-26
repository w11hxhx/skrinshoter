using System.Diagnostics;

namespace Common;

public class StoppableStopwatch
{
	private bool isStarted;

	private long startTimestamp;

	private long stopTimestamp;

	private readonly object lockObject = new object();

	public long Frequency { get; } = Stopwatch.Frequency;

	public bool IsStarted
	{
		get
		{
			lock (lockObject)
			{
				return isStarted;
			}
		}
	}

	public void Start()
	{
		lock (lockObject)
		{
			if (!isStarted)
			{
				isStarted = true;
				startTimestamp = Stopwatch.GetTimestamp() - (stopTimestamp - startTimestamp);
			}
		}
	}

	public void Stop()
	{
		lock (lockObject)
		{
			if (isStarted)
			{
				isStarted = false;
				stopTimestamp = Stopwatch.GetTimestamp();
			}
		}
	}

	public long GetTimestamp()
	{
		lock (lockObject)
		{
			return (isStarted ? Stopwatch.GetTimestamp() : stopTimestamp) - startTimestamp;
		}
	}

	public long GetTimestamp(out bool isStarted)
	{
		lock (lockObject)
		{
			isStarted = this.isStarted;
			return (this.isStarted ? Stopwatch.GetTimestamp() : stopTimestamp) - startTimestamp;
		}
	}
}
