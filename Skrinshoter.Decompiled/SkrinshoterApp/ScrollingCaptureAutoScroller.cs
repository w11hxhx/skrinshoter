using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MouseHook;
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace SkrinshoterApp;

public sealed class ScrollingCaptureAutoScroller : SafeDisposable
{
	private static readonly TimeSpan ScrollEventInterval = TimeSpan.FromMilliseconds(80.0);

	private const int WheelDelta = -120;

	private readonly ILogger logger;

	private readonly string monitorName;

	private readonly Rectangle captureArea;

	private readonly ScrollingCaptureMouseInputSuppressor mouseInputSuppressor;

	private readonly Timer timer;

	private bool isStarted;

	public event EventHandler<MousePositionReportEventArgs> MousePositionReport;

	public ScrollingCaptureAutoScroller(string monitorName, Rectangle captureArea, ILoggerFactory loggerFactory)
	{
		if (monitorName == null)
		{
			throw new ArgumentNullException("monitorName");
		}
		if (captureArea.Width <= 0 || captureArea.Height <= 0)
		{
			throw new ArgumentOutOfRangeException("captureArea");
		}
		this.monitorName = monitorName;
		this.captureArea = captureArea;
		if (loggerFactory == null)
		{
			loggerFactory = NullLoggerFactory.Instance;
		}
		logger = loggerFactory.CreateLogger<ScrollingCaptureAutoScroller>();
		try
		{
			mouseInputSuppressor = new ScrollingCaptureMouseInputSuppressor(monitorName, loggerFactory.CreateLogger<ScrollingCaptureMouseInputSuppressor>());
			timer = new Timer(TimerCallback, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public void Start()
	{
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed && !isStarted)
			{
				timer.Change(ScrollEventInterval, Timeout.InfiniteTimeSpan);
				isStarted = true;
			}
		}
	}

	private void TimerCallback(object state)
	{
		try
		{
			if (!IsDisposed)
			{
				bool isMouseInsideCaptureArea = GetIsMouseInsideCaptureArea();
				MousePositionReportEventArgs e = new MousePositionReportEventArgs(isMouseInsideCaptureArea);
				MousePositionReport?.Invoke(this, e);
				if (!IsDisposed && e.CanInject && isMouseInsideCaptureArea && GetIsMouseInsideCaptureArea())
				{
					InjectMouseWheel();
				}
			}
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Error in timer callback");
		}
		finally
		{
			lock (InternalDisposeLockObject)
			{
				if (!IsDisposed)
				{
					timer.Change(ScrollEventInterval, Timeout.InfiniteTimeSpan);
				}
			}
		}
	}

	private bool GetIsMouseInsideCaptureArea()
	{
		if (!PInvoke.GetCursorPos(out var lpPoint))
		{
			throw new WinApiException("GetCursorPos()", new Win32Exception());
		}
		HMONITOR hMONITOR = PInvoke.MonitorFromPoint(lpPoint, MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONULL);
		if (hMONITOR.IsNull)
		{
			return false;
		}
		MonitorInfo monitorInfo = new MonitorInfo(hMONITOR);
		if (monitorInfo.MonitorName != monitorName)
		{
			return false;
		}
		Rectangle rectangle = monitorInfo.GetRectangle();
		lpPoint.Offset(-rectangle.X, -rectangle.Y);
		return captureArea.Contains(lpPoint);
	}

	private void InjectMouseWheel()
	{
		Span<INPUT> span = stackalloc INPUT[1];
		span[0].type = INPUT_TYPE.INPUT_MOUSE;
		span[0].Anonymous.mi = new MOUSEINPUT
		{
			mouseData = 4294967176u,
			dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_WHEEL,
			dwExtraInfo = 1396920908u
		};
		if (PInvoke.SendInput(span, Marshal.SizeOf<INPUT>()) != 1)
		{
			logger.LogWarning("SendInput(MOUSEEVENTF_WHEEL) failed: {reason}", new Win32Exception().Message);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			timer?.Dispose();
			mouseInputSuppressor?.Dispose();
		}
	}
}
