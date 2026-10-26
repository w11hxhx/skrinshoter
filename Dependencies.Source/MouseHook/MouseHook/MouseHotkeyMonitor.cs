using System;
using Common;
using Microsoft.Extensions.Logging;
using Windows.Win32.UI.WindowsAndMessaging;

namespace MouseHook;

public class MouseHotkeyMonitor : MouseHookBase
{
	private bool leftButtonDown;

	private bool rightButtonDown;

	private bool leftButtonSuppressed;

	private bool rightButtonSuppressed;

	private bool hotkeyActive;

	private readonly AsyncEventInvoker<EventArgs> hotkeyPressedEventInvoker;

	public event EventHandler<EventArgs> HotkeyPressed
	{
		add
		{
			hotkeyPressedEventInvoker.Subscribe(value);
		}
		remove
		{
			hotkeyPressedEventInvoker.Unsubscribe(value);
		}
	}

	public MouseHotkeyMonitor(ILogger<MouseHotkeyMonitor> logger)
		: base(logger)
	{
		hotkeyPressedEventInvoker = new AsyncEventInvoker<EventArgs>(base.logger, "HotkeyPressed");
	}

	public void Start()
	{
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed)
			{
				StartMouseHook();
			}
		}
	}

	public void Stop()
	{
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed)
			{
				StopMouseHook();
			}
		}
	}

	private protected override bool OnMouseMessage(uint message, ref MSLLHOOKSTRUCT data)
	{
		switch (message)
		{
		case 513u:
			leftButtonDown = true;
			if (rightButtonDown && !hotkeyActive)
			{
				hotkeyActive = true;
				hotkeyPressedEventInvoker.InvokeAsync(this, EventArgs.Empty);
				leftButtonSuppressed = true;
			}
			else
			{
				leftButtonSuppressed = false;
			}
			break;
		case 514u:
			leftButtonDown = false;
			if (!rightButtonDown)
			{
				hotkeyActive = false;
			}
			if (leftButtonSuppressed)
			{
				leftButtonSuppressed = false;
			}
			break;
		case 516u:
			rightButtonDown = true;
			if (leftButtonDown && !hotkeyActive)
			{
				hotkeyActive = true;
				hotkeyPressedEventInvoker.InvokeAsync(this, EventArgs.Empty);
				rightButtonSuppressed = true;
			}
			else
			{
				rightButtonSuppressed = false;
			}
			break;
		case 517u:
			rightButtonDown = false;
			if (!leftButtonDown)
			{
				hotkeyActive = false;
			}
			if (rightButtonSuppressed)
			{
				rightButtonSuppressed = false;
			}
			break;
		}
		return false;
	}
}
