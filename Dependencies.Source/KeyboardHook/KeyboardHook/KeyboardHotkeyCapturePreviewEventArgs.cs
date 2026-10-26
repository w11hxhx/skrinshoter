using System;

namespace KeyboardHook;

public sealed class KeyboardHotkeyCapturePreviewEventArgs : EventArgs
{
	public int VirtualKeyCode { get; }

	public KeyModifiers Modifiers { get; }

	public KeyboardHotkeyCapturePreviewEventArgs(int virtualKeyCode, KeyModifiers modifiers)
	{
		VirtualKeyCode = virtualKeyCode;
		Modifiers = modifiers;
	}
}
