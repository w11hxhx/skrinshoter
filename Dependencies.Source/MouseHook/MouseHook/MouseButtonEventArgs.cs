using System;
using System.Drawing;

namespace MouseHook;

public class MouseButtonEventArgs : EventArgs
{
	public MouseButton ChangedButton { get; }

	public MouseButtonState ButtonState { get; }

	public Point Position { get; }

	public MouseButtonEventArgs(MouseButton changedButton, MouseButtonState buttonState, Point position)
	{
		ChangedButton = changedButton;
		ButtonState = buttonState;
		Position = position;
	}
}
