using System;
using System.Collections.Generic;
using System.Windows.Input;
using Common;

namespace SkrinshoterApp;

public readonly struct Hotkey : IEquatable<Hotkey>
{
	public static readonly Hotkey Empty = new Hotkey(Key.None);

	private static readonly HashSet<Key> ValidOneKeyHotkeys = new HashSet<Key>
	{
		Key.Snapshot,
		Key.Scroll,
		Key.Pause,
		Key.F1,
		Key.F2,
		Key.F3,
		Key.F4,
		Key.F5,
		Key.F6,
		Key.F7,
		Key.F8,
		Key.F9,
		Key.F10,
		Key.F11,
		Key.F12,
		Key.F13,
		Key.F14,
		Key.F15,
		Key.F16,
		Key.F17,
		Key.F18,
		Key.F19,
		Key.F20,
		Key.F21,
		Key.F22,
		Key.F23,
		Key.F24,
		Key.Insert,
		Key.Home,
		Key.End,
		Key.Prior,
		Key.Next,
		Key.NumLock,
		Key.Divide,
		Key.Multiply,
		Key.Subtract,
		Key.Add,
		Key.Capital,
		Key.Tab
	};

	public ModifierKeys Modifiers { get; }

	public Key Key { get; }

	public bool IsEmpty
	{
		get
		{
			if (Modifiers == ModifierKeys.None)
			{
				return Key == Key.None;
			}
			return false;
		}
	}

	public Hotkey(Key key)
		: this(ModifierKeys.None, key)
	{
	}

	public Hotkey(ModifierKeys modifiers, Key key)
	{
		Modifiers = modifiers;
		Key = key;
	}

	public bool Equals(Hotkey other)
	{
		if (Modifiers == other.Modifiers)
		{
			return Key == other.Key;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj is Hotkey other)
		{
			return Equals(other);
		}
		return false;
	}

	public static bool operator ==(Hotkey a, Hotkey b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(Hotkey a, Hotkey b)
	{
		return !(a == b);
	}

	public override int GetHashCode()
	{
		return ((int)Modifiers * 397) ^ (int)Key;
	}

	public static bool IsValid(Hotkey hotkey)
	{
		if (hotkey.IsEmpty)
		{
			return true;
		}
		if (hotkey.Modifiers == ModifierKeys.None)
		{
			return ValidOneKeyHotkeys.Contains(hotkey.Key);
		}
		return !hotkey.Key.In(Key.None, Key.LeftCtrl, Key.RightCtrl, Key.LeftAlt, Key.RightAlt, Key.LeftShift, Key.RightShift, Key.LWin, Key.RWin);
	}

	public override string ToString()
	{
		if (IsEmpty)
		{
			return string.Empty;
		}
		List<string> list = new List<string>();
		if (Modifiers.HasFlag(ModifierKeys.Control))
		{
			list.Add("Ctrl");
		}
		if (Modifiers.HasFlag(ModifierKeys.Alt))
		{
			list.Add("Alt");
		}
		if (Modifiers.HasFlag(ModifierKeys.Shift))
		{
			list.Add("Shift");
		}
		if (Modifiers.HasFlag(ModifierKeys.Windows))
		{
			list.Add("Win");
		}
		list.Add(KeyToString(Key));
		return string.Join("+", list);
	}

	public string ToDisplaySuffix()
	{
		if (!IsEmpty)
		{
			return " (" + ToString() + ")";
		}
		return string.Empty;
	}

	private static string KeyToString(Key key)
	{
		switch (key)
		{
		case Key.D0:
		case Key.D1:
		case Key.D2:
		case Key.D3:
		case Key.D4:
		case Key.D5:
		case Key.D6:
		case Key.D7:
		case Key.D8:
		case Key.D9:
			return key.ToString().TrimStart('D');
		case Key.Return:
			return "Enter";
		case Key.Capital:
			return "CapsLock";
		case Key.Prior:
			return "PageUp";
		case Key.Next:
			return "PageDown";
		case Key.Snapshot:
			return "PrtScr";
		case Key.Scroll:
			return "ScrollLock";
		case Key.Divide:
			return "NumPad/";
		case Key.Multiply:
			return "NumPad*";
		case Key.Subtract:
			return "NumPad-";
		case Key.Add:
			return "NumPad+";
		default:
			return key.ToString();
		}
	}
}
