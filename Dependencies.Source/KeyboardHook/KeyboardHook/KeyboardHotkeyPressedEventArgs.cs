using System;

namespace KeyboardHook;

public sealed class KeyboardHotkeyPressedEventArgs : EventArgs
{
	public int HotkeyId { get; }

	public KeyboardHotkeyPressedEventArgs(int hotkeyId)
	{
		HotkeyId = hotkeyId;
	}
}
