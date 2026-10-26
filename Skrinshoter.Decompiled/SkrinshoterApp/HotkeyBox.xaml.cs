using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using KeyboardHook;

namespace SkrinshoterApp;

public partial class HotkeyBox : UserControl
{
	public static readonly DependencyProperty HotkeyProperty = DependencyProperty.Register("Hotkey", typeof(Hotkey), typeof(HotkeyBox), new FrameworkPropertyMetadata(Hotkey.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHotkeyChanged));

	public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.Register("DisplayText", typeof(string), typeof(HotkeyBox), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty KeyboardHotkeyMonitorProperty = DependencyProperty.Register("KeyboardHotkeyMonitor", typeof(KeyboardHotkeyMonitor), typeof(HotkeyBox), new PropertyMetadata(null));

	private bool isCapturing;

	private bool clearOnFocusFromClick;

	private Hotkey prevHotkey;

	private Hotkey pendingHotkey;

	private CancellationTokenSource captureCancellation;

	private KeyboardHotkeyMonitor subscribedMonitor;

	public Hotkey Hotkey
	{
		get
		{
			return (Hotkey)GetValue(HotkeyProperty);
		}
		set
		{
			SetValue(HotkeyProperty, value);
		}
	}

	public string DisplayText
	{
		get
		{
			return (string)GetValue(DisplayTextProperty);
		}
		private set
		{
			SetValue(DisplayTextProperty, value);
		}
	}

	public KeyboardHotkeyMonitor KeyboardHotkeyMonitor
	{
		get
		{
			return (KeyboardHotkeyMonitor)GetValue(KeyboardHotkeyMonitorProperty);
		}
		set
		{
			SetValue(KeyboardHotkeyMonitorProperty, value);
		}
	}

	public HotkeyBox()
	{
		InitializeComponent();
		UpdateDisplay();
		Unloaded += HotkeyBox_Unloaded;
	}

	private void HotkeyBox_Unloaded(object sender, RoutedEventArgs e)
	{
		CancelLowLevelCapture();
		isCapturing = false;
		pendingHotkey = Hotkey.Empty;
	}

	private static void OnHotkeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		HotkeyBox hotkeyBox = (HotkeyBox)d;
		if (!hotkeyBox.isCapturing)
		{
			hotkeyBox.UpdateDisplay();
		}
	}

	private void UpdateDisplay()
	{
		DisplayText = (Hotkey.IsEmpty ? string.Empty : Hotkey.ToString());
		MoveCaretToEnd();
	}

	private void MoveCaretToEnd()
	{
		if (PART_TextBox.IsKeyboardFocused)
		{
			PART_TextBox.CaretIndex = PART_TextBox.Text.Length;
		}
	}

	private void RaiseAutoSaveRequest()
	{
		RaiseEvent(new RoutedEventArgs(AutoSaveOnInputBehavior.AutoSaveRequestEvent, this));
	}

	private void SetHotkeyValue(Hotkey value)
	{
		SetCurrentValue(HotkeyProperty, value);
		BindingOperations.GetBindingExpression(this, HotkeyProperty)?.UpdateSource();
	}

	private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		e.Handled = true;
		prevHotkey = Hotkey;
		SetHotkeyValue(Hotkey.Empty);
		if (PART_TextBox.IsKeyboardFocused)
		{
			StartCaptureCleared();
			MoveCaretToEnd();
			return;
		}
		clearOnFocusFromClick = true;
		if (!PART_TextBox.Focus())
		{
			clearOnFocusFromClick = false;
			SetHotkeyValue(prevHotkey);
		}
	}

	private void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (!clearOnFocusFromClick)
		{
			e.Handled = true;
		}
	}

	private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (clearOnFocusFromClick)
		{
			StartCaptureCleared();
			clearOnFocusFromClick = false;
			MoveCaretToEnd();
		}
	}

	private void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (isCapturing)
		{
			CancelCaptureInternal();
		}
	}

	private void OnClearClick(object sender, RoutedEventArgs e)
	{
		CancelLowLevelCapture();
		isCapturing = false;
		pendingHotkey = Hotkey.Empty;
		SetHotkeyValue(Hotkey.Empty);
		RaiseAutoSaveRequest();
		UpdateDisplay();
		prevHotkey = Hotkey.Empty;
	}

	private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		e.Handled = true;
	}

	private void OnPreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (((e.Key == Key.System) ? e.SystemKey : e.Key) == Key.Escape)
		{
			e.Handled = true;
			if (isCapturing)
			{
				CancelCaptureAndDefocus();
			}
			else
			{
				Keyboard.ClearFocus();
			}
		}
		else
		{
			e.Handled = true;
		}
	}

	private void StartCaptureCleared()
	{
		isCapturing = true;
		pendingHotkey = Hotkey.Empty;
		DisplayText = string.Empty;
		MoveCaretToEnd();
		StartLowLevelCapture();
	}

	private void FinishCaptureCommit()
	{
		if (isCapturing)
		{
			CancelLowLevelCapture();
			Hotkey hotkey = prevHotkey;
			if (!pendingHotkey.IsEmpty && Hotkey.IsValid(pendingHotkey) && IsHotkeyAvailable(pendingHotkey))
			{
				hotkey = pendingHotkey;
			}
			SetHotkeyValue(hotkey);
			isCapturing = false;
			pendingHotkey = Hotkey.Empty;
			DisplayText = (Hotkey.IsEmpty ? string.Empty : Hotkey.ToString());
			RaiseAutoSaveRequest();
		}
	}

	private void CancelCaptureAndDefocus()
	{
		CancelCaptureInternal();
		Keyboard.ClearFocus();
	}

	private void CancelCaptureInternal()
	{
		CancelLowLevelCapture();
		isCapturing = false;
		pendingHotkey = Hotkey.Empty;
		SetHotkeyValue(prevHotkey);
		DisplayText = (prevHotkey.IsEmpty ? string.Empty : prevHotkey.ToString());
	}

	private void StartLowLevelCapture()
	{
		KeyboardHotkeyMonitor keyboardHotkeyMonitor = KeyboardHotkeyMonitor;
		if (keyboardHotkeyMonitor == null)
		{
			CancelCaptureAndDefocus();
			return;
		}
		CancelLowLevelCapture();
		SubscribeCapturePreview(keyboardHotkeyMonitor);
		captureCancellation = new CancellationTokenSource();
		Task<KeyboardHotkey> task = keyboardHotkeyMonitor.CaptureHotkeyAsync(captureCancellation.Token);
		AwaitCapturedHotkeyAsync(task, captureCancellation);
	}

	private async Task AwaitCapturedHotkeyAsync(Task<KeyboardHotkey> task, CancellationTokenSource cts)
	{
		try
		{
			KeyboardHotkey captured = await task.ConfigureAwait(continueOnCapturedContext: false);
			await Dispatcher.InvokeAsync(() =>
			{
				if (isCapturing && captureCancellation == cts)
				{
					Key key = KeyInterop.KeyFromVirtualKey(captured.VirtualKeyCode);
					if (key == Key.Escape)
					{
						CancelCaptureAndDefocus();
					}
					else
					{
						ModifierKeys modifiers = ConvertModifiersFromMonitor(captured.Modifiers);
						pendingHotkey = new Hotkey(modifiers, key);
						DisplayText = pendingHotkey.ToString();
						MoveCaretToEnd();
						FinishCaptureCommit();
						Keyboard.ClearFocus();
					}
				}
			});
		}
		catch (TaskCanceledException)
		{
			Dispatcher.BeginInvoke((Action)(() =>
			{
				if (isCapturing && captureCancellation == cts)
				{
					CancelCaptureInternal();
				}
			}));
		}
		catch
		{
			await Dispatcher.InvokeAsync(() =>
			{
				if (captureCancellation == cts)
				{
					CancelCaptureInternal();
				}
			});
		}
	}

	private void CancelLowLevelCapture()
	{
		if (captureCancellation != null)
		{
			captureCancellation.Cancel();
			captureCancellation.Dispose();
			captureCancellation = null;
		}
		UnsubscribeCapturePreview();
	}

	private void SubscribeCapturePreview(KeyboardHotkeyMonitor monitor)
	{
		if (subscribedMonitor != monitor)
		{
			if (subscribedMonitor != null)
			{
				subscribedMonitor.HotkeyCapturePreview -= OnCapturePreviewChanged;
			}
			subscribedMonitor = monitor;
			subscribedMonitor.HotkeyCapturePreview += OnCapturePreviewChanged;
		}
	}

	private void UnsubscribeCapturePreview()
	{
		if (subscribedMonitor != null)
		{
			subscribedMonitor.HotkeyCapturePreview -= OnCapturePreviewChanged;
			subscribedMonitor = null;
		}
	}

	private void OnCapturePreviewChanged(object sender, KeyboardHotkeyCapturePreviewEventArgs e)
	{
		if (!isCapturing || sender != subscribedMonitor)
		{
			return;
		}
		Dispatcher.BeginInvoke((Action)(() =>
		{
			if (isCapturing && sender == subscribedMonitor)
			{
				ModifierKeys modifierKeys = ConvertModifiersFromMonitor(e.Modifiers);
				if (e.VirtualKeyCode != 0)
				{
					pendingHotkey = new Hotkey(modifierKeys, KeyInterop.KeyFromVirtualKey(e.VirtualKeyCode));
					DisplayText = pendingHotkey.ToString();
					MoveCaretToEnd();
				}
				else
				{
					DisplayText = ((modifierKeys == ModifierKeys.None) ? string.Empty : ModifiersToString(modifierKeys));
					MoveCaretToEnd();
				}
			}
		}));
	}

	private bool IsHotkeyAvailable(Hotkey hotkey)
	{
		if (hotkey.IsEmpty)
		{
			return true;
		}
		KeyboardHotkeyMonitor keyboardHotkeyMonitor = KeyboardHotkeyMonitor;
		if (keyboardHotkeyMonitor == null)
		{
			return true;
		}
		return !keyboardHotkeyMonitor.IsHotkeyRegistered(KeyInterop.VirtualKeyFromKey(hotkey.Key), ConvertModifiersToMonitor(hotkey.Modifiers));
	}

	private static ModifierKeys ConvertModifiersFromMonitor(KeyModifiers modifiers)
	{
		ModifierKeys modifierKeys = ModifierKeys.None;
		if (modifiers.HasFlag(KeyModifiers.Control))
		{
			modifierKeys |= ModifierKeys.Control;
		}
		if (modifiers.HasFlag(KeyModifiers.Alt))
		{
			modifierKeys |= ModifierKeys.Alt;
		}
		if (modifiers.HasFlag(KeyModifiers.Shift))
		{
			modifierKeys |= ModifierKeys.Shift;
		}
		if (modifiers.HasFlag(KeyModifiers.Windows))
		{
			modifierKeys |= ModifierKeys.Windows;
		}
		return modifierKeys;
	}

	private static KeyModifiers ConvertModifiersToMonitor(ModifierKeys modifiers)
	{
		KeyModifiers keyModifiers = KeyModifiers.None;
		if (modifiers.HasFlag(ModifierKeys.Control))
		{
			keyModifiers |= KeyModifiers.Control;
		}
		if (modifiers.HasFlag(ModifierKeys.Alt))
		{
			keyModifiers |= KeyModifiers.Alt;
		}
		if (modifiers.HasFlag(ModifierKeys.Shift))
		{
			keyModifiers |= KeyModifiers.Shift;
		}
		if (modifiers.HasFlag(ModifierKeys.Windows))
		{
			keyModifiers |= KeyModifiers.Windows;
		}
		return keyModifiers;
	}

	private string ModifiersToString(ModifierKeys mods)
	{
		if (mods == ModifierKeys.None)
		{
			return string.Empty;
		}
		List<string> list = new List<string>();
		if (mods.HasFlag(ModifierKeys.Control))
		{
			list.Add("Ctrl");
		}
		if (mods.HasFlag(ModifierKeys.Alt))
		{
			list.Add("Alt");
		}
		if (mods.HasFlag(ModifierKeys.Shift))
		{
			list.Add("Shift");
		}
		if (mods.HasFlag(ModifierKeys.Windows))
		{
			list.Add("Win");
		}
		return string.Join("+", list);
	}

}
