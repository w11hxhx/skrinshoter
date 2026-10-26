using System;
using Common;
using Microsoft.Extensions.Logging;
using Windows.Win32.UI.WindowsAndMessaging;

namespace MouseHook;

public class MouseButtonMonitor : MouseHookBase
{
	private readonly AsyncEventInvoker<MouseButtonEventArgs> mouseButtonEventInvoker;

	public event EventHandler<MouseButtonEventArgs> MouseButtonEvent
	{
		add
		{
			lock (InternalDisposeLockObject)
			{
				if (!IsDisposed)
				{
					mouseButtonEventInvoker.Subscribe(value);
					StartMouseHook();
				}
			}
		}
		remove
		{
			lock (InternalDisposeLockObject)
			{
				if (!IsDisposed)
				{
					mouseButtonEventInvoker.Unsubscribe(value);
					if (mouseButtonEventInvoker.Subscribers.Count == 0)
					{
						StopMouseHook();
					}
				}
			}
		}
	}

	public MouseButtonMonitor(ILogger<MouseButtonMonitor> logger)
		: base(logger)
	{
		mouseButtonEventInvoker = new AsyncEventInvoker<MouseButtonEventArgs>(base.logger, "MouseButtonEvent");
	}

	private protected override bool OnMouseMessage(uint message, ref MSLLHOOKSTRUCT data)
	{
		switch (message)
		{
		case 513u:
			mouseButtonEventInvoker.InvokeAsync(this, new MouseButtonEventArgs(MouseButton.Left, MouseButtonState.Pressed, data.pt));
			break;
		case 514u:
			mouseButtonEventInvoker.InvokeAsync(this, new MouseButtonEventArgs(MouseButton.Left, MouseButtonState.Released, data.pt));
			break;
		case 516u:
			mouseButtonEventInvoker.InvokeAsync(this, new MouseButtonEventArgs(MouseButton.Right, MouseButtonState.Pressed, data.pt));
			break;
		case 517u:
			mouseButtonEventInvoker.InvokeAsync(this, new MouseButtonEventArgs(MouseButton.Right, MouseButtonState.Released, data.pt));
			break;
		}
		return false;
	}
}
