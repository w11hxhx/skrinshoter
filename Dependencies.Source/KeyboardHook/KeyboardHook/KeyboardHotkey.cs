namespace KeyboardHook;

public sealed class KeyboardHotkey
{
	public int VirtualKeyCode { get; }

	public KeyModifiers Modifiers { get; }

	public KeyboardHotkey(int virtualKeyCode, KeyModifiers modifiers)
	{
		VirtualKeyCode = virtualKeyCode;
		Modifiers = modifiers;
	}
}
