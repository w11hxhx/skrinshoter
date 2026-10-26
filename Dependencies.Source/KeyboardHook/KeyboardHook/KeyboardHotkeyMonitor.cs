using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

namespace KeyboardHook;

public sealed class KeyboardHotkeyMonitor : SafeDisposable
{
	private sealed class CaptureRequest
	{
		public TaskCompletionSource<KeyboardHotkey> Completion { get; } = new TaskCompletionSource<KeyboardHotkey>(TaskCreationOptions.RunContinuationsAsynchronously);

		public CancellationTokenRegistration CancellationRegistration { get; set; }

		public void Complete(KeyboardHotkey hotkey)
		{
			Completion.TrySetResult(hotkey);
			CancellationRegistration.Dispose();
		}

		public void Cancel()
		{
			Completion.TrySetCanceled();
			CancellationRegistration.Dispose();
		}
	}

	private static readonly TimeSpan RestartHookInterval = TimeSpan.FromSeconds(5.0);

	private readonly ILogger logger;

	private readonly Dictionary<KeyModifiers, int>[] hotkeys = new Dictionary<KeyModifiers, int>[256];

	private readonly Thread thread;

	private readonly TaskCompletionSource<bool> threadInitialized;

	private static readonly FreeLibrarySafeHandle userLibrary = PInvoke.LoadLibrary("User32");

	private readonly HOOKPROC hookProc;

	private HHOOK hHook;

	private volatile Dispatcher dispatcher;

	private DispatcherTimer restartTimer;

	private CaptureRequest captureRequest;

	private readonly AsyncEventInvoker<KeyboardHotkeyPressedEventArgs> hotkeyPressedEventInvoker;

	private readonly AsyncEventInvoker<KeyboardHotkeyCapturePreviewEventArgs> hotkeyCapturePreviewEventInvoker;

	public event EventHandler<KeyboardHotkeyPressedEventArgs> HotkeyPressed
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

	public event EventHandler<KeyboardHotkeyCapturePreviewEventArgs> HotkeyCapturePreview
	{
		add
		{
			hotkeyCapturePreviewEventInvoker.Subscribe(value);
		}
		remove
		{
			hotkeyCapturePreviewEventInvoker.Unsubscribe(value);
		}
	}

	private void RaiseHotkeyPressed(int hotkeyId)
	{
		hotkeyPressedEventInvoker.InvokeAsync(this, new KeyboardHotkeyPressedEventArgs(hotkeyId));
	}

	private void RaiseHotkeyCapturePreview(int virtualKeyCode = 0)
	{
		hotkeyCapturePreviewEventInvoker.InvokeAsync(this, new KeyboardHotkeyCapturePreviewEventArgs(virtualKeyCode, GetCurrentKeyModifiers()));
	}

	public KeyboardHotkeyMonitor(ILogger<KeyboardHotkeyMonitor> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		hookProc = LowLevelKeyboardProc;
		hotkeyPressedEventInvoker = new AsyncEventInvoker<KeyboardHotkeyPressedEventArgs>(this.logger, "HotkeyPressed");
		hotkeyCapturePreviewEventInvoker = new AsyncEventInvoker<KeyboardHotkeyCapturePreviewEventArgs>(this.logger, "HotkeyCapturePreview");
		threadInitialized = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
		thread = new Thread(ThreadProc)
		{
			Name = "Low-level keyboard hook",
			IsBackground = true,
			Priority = ThreadPriority.Highest
		};
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	public void RegisterHotkey(int virtualKeyCode, KeyModifiers modifiers, int hotkeyId)
	{
		if (virtualKeyCode < 0 || virtualKeyCode > 255)
		{
			throw new ArgumentOutOfRangeException("virtualKeyCode");
		}
		if (!threadInitialized.Task.IsCompleted)
		{
			threadInitialized.Task.Wait();
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return;
			}
			ExecuteOnHookThread(() =>
			{
				Dictionary<KeyModifiers, int> dictionary = hotkeys[virtualKeyCode];
				if (dictionary == null)
				{
					dictionary = new Dictionary<KeyModifiers, int>();
					hotkeys[virtualKeyCode] = dictionary;
				}
				if (dictionary.ContainsKey(modifiers))
				{
					throw new InvalidOperationException("Hotkey is already registered.");
				}
				dictionary[modifiers] = hotkeyId;
			});
		}
	}

	public void UnregisterHotkey(int virtualKeyCode, KeyModifiers modifiers)
	{
		if (virtualKeyCode < 0 || virtualKeyCode > 255)
		{
			throw new ArgumentOutOfRangeException("virtualKeyCode");
		}
		if (!threadInitialized.Task.IsCompleted)
		{
			threadInitialized.Task.Wait();
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return;
			}
			ExecuteOnHookThread(() =>
			{
				Dictionary<KeyModifiers, int> dictionary = hotkeys[virtualKeyCode];
				if (dictionary != null)
				{
					dictionary.Remove(modifiers);
					if (dictionary.Count == 0)
					{
						hotkeys[virtualKeyCode] = null;
					}
				}
			});
		}
	}

	public bool IsHotkeyRegistered(int virtualKeyCode, KeyModifiers modifiers)
	{
		if (virtualKeyCode < 0 || virtualKeyCode > 255)
		{
			throw new ArgumentOutOfRangeException("virtualKeyCode");
		}
		if (!threadInitialized.Task.IsCompleted)
		{
			threadInitialized.Task.Wait();
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return false;
			}
			return ExecuteOnHookThread(() => hotkeys[virtualKeyCode]?.ContainsKey(modifiers) ?? false);
		}
	}

	public Task<KeyboardHotkey> CaptureHotkeyAsync(CancellationToken cancellationToken)
	{
		if (!threadInitialized.Task.IsCompleted)
		{
			threadInitialized.Task.Wait();
		}
		if (cancellationToken.IsCancellationRequested)
		{
			return Task.FromCanceled<KeyboardHotkey>(cancellationToken);
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return Task.FromCanceled<KeyboardHotkey>(new CancellationToken(canceled: true));
			}
			CaptureRequest request = new CaptureRequest();
			ExecuteOnHookThread(() =>
			{
				captureRequest?.Cancel();
				captureRequest = request;
				RaiseHotkeyCapturePreview();
				if (cancellationToken.CanBeCanceled)
				{
					request.CancellationRegistration = cancellationToken.Register(() =>
					{
						try
						{
							dispatcher?.BeginInvoke((Action)(() =>
							{
								if (captureRequest == request)
								{
									captureRequest.Cancel();
									captureRequest = null;
								}
							}));
						}
						catch
						{
						}
					});
				}
			});
			return request.Completion.Task;
		}
	}

	private void ThreadProc()
	{
		try
		{
			try
			{
				dispatcher = Dispatcher.CurrentDispatcher;
				hHook = PInvoke.SetWindowsHookEx(WINDOWS_HOOK_ID.WH_KEYBOARD_LL, hookProc, (HINSTANCE)userLibrary.DangerousGetHandle(), 0u);
				if (hHook.IsNull)
				{
					throw new WinApiException("SetWindowsHookEx(WH_KEYBOARD_LL)", new Win32Exception());
				}
				restartTimer = new DispatcherTimer
				{
					Interval = RestartHookInterval
				};
				restartTimer.Tick += RestartTimerTick;
				restartTimer.Start();
				logger.LogInformation("Keyboard hook started");
				threadInitialized.TrySetResult(result: true);
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to start keyboard hook");
				threadInitialized.TrySetResult(result: true);
				return;
			}
			try
			{
				Dispatcher.Run();
			}
			catch (Exception exception2)
			{
				logger.LogError(exception2, "Error in keyboard hook thread event loop");
			}
		}
		finally
		{
			if (!hHook.IsNull)
			{
				if ((bool)PInvoke.UnhookWindowsHookEx(hHook))
				{
					logger.LogInformation("Keyboard hook stopped");
				}
				else
				{
					logger.LogError(new WinApiException("UnhookWindowsHookEx()", new Win32Exception()), "Failed to stop keyboard hook");
				}
				hHook = HHOOK.Null;
			}
			if (restartTimer != null)
			{
				restartTimer.Stop();
				restartTimer.Tick -= RestartTimerTick;
				restartTimer = null;
			}
			captureRequest?.Cancel();
			captureRequest = null;
			dispatcher = null;
		}
	}

	private void RestartTimerTick(object sender, EventArgs e)
	{
		if (IsDisposed || hHook.IsNull)
		{
			return;
		}
		HHOOK hHOOK = PInvoke.SetWindowsHookEx(WINDOWS_HOOK_ID.WH_KEYBOARD_LL, hookProc, (HINSTANCE)userLibrary.DangerousGetHandle(), 0u);
		if (hHOOK.IsNull)
		{
			logger.LogWarning("Failed to restart keyboard hook");
			return;
		}
		if (!PInvoke.UnhookWindowsHookEx(hHook))
		{
			logger.LogWarning("Failed to remove old keyboard hook");
		}
		hHook = hHOOK;
	}

	private unsafe LRESULT LowLevelKeyboardProc(int code, WPARAM wParam, LPARAM lParam)
	{
		if (code < 0)
		{
			return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
		}
		bool flag = wParam == 256u || wParam == 260u;
		bool flag2 = wParam == 257u || wParam == 261u;
		if (!flag && !flag2)
		{
			return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
		}
		int vkCode = (int)((KBDLLHOOKSTRUCT*)(void*)(IntPtr)(nint)lParam)->vkCode;
		if (vkCode < 0 || vkCode > 255)
		{
			return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
		}
		if (captureRequest != null)
		{
			if (IsModifierKey(vkCode))
			{
				try
				{
					dispatcher?.BeginInvoke((Action)(() =>
					{
						if (captureRequest != null)
						{
							RaiseHotkeyCapturePreview();
						}
					}));
				}
				catch
				{
				}
				return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
			}
			if (!flag)
			{
				return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
			}
			RaiseHotkeyCapturePreview(vkCode);
			captureRequest.Complete(new KeyboardHotkey(vkCode, GetCurrentKeyModifiers()));
			captureRequest = null;
			return (LRESULT)1;
		}
		if (!flag)
		{
			return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
		}
		Dictionary<KeyModifiers, int> dictionary = hotkeys[vkCode];
		if (dictionary == null)
		{
			return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
		}
		if (!dictionary.TryGetValue(GetCurrentKeyModifiers(), out var value))
		{
			return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
		}
		RaiseHotkeyPressed(value);
		return (LRESULT)1;
	}

	private static KeyModifiers GetCurrentKeyModifiers()
	{
		KeyModifiers keyModifiers = KeyModifiers.None;
		if ((PInvoke.GetAsyncKeyState(17) & 0x8000) != 0)
		{
			keyModifiers |= KeyModifiers.Control;
		}
		if ((PInvoke.GetAsyncKeyState(18) & 0x8000) != 0)
		{
			keyModifiers |= KeyModifiers.Alt;
		}
		if ((PInvoke.GetAsyncKeyState(16) & 0x8000) != 0)
		{
			keyModifiers |= KeyModifiers.Shift;
		}
		if ((PInvoke.GetAsyncKeyState(91) & 0x8000) != 0 || (PInvoke.GetAsyncKeyState(92) & 0x8000) != 0)
		{
			keyModifiers |= KeyModifiers.Windows;
		}
		return keyModifiers;
	}

	private static bool IsModifierKey(int virtualKeyCode)
	{
		VIRTUAL_KEY vIRTUAL_KEY = (VIRTUAL_KEY)virtualKeyCode;
		if (vIRTUAL_KEY - 16 <= VIRTUAL_KEY.VK_RBUTTON || vIRTUAL_KEY - 91 <= VIRTUAL_KEY.VK_LBUTTON || vIRTUAL_KEY - 160 <= VIRTUAL_KEY.VK_XBUTTON1)
		{
			return true;
		}
		return false;
	}

	private void ExecuteOnHookThread(Action action)
	{
		(dispatcher ?? throw new InvalidOperationException("Keyboard hook thread dispatcher is null.")).Invoke(action);
	}

	private T ExecuteOnHookThread<T>(Func<T> func)
	{
		return (dispatcher ?? throw new InvalidOperationException("Keyboard hook thread dispatcher is null.")).Invoke(func);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (!threadInitialized.Task.IsCompleted)
			{
				threadInitialized.Task.Wait();
			}
			try
			{
				dispatcher?.InvokeShutdown();
			}
			catch
			{
			}
			if (!thread.Join(TimeSpan.FromSeconds(5.0)))
			{
				logger.LogWarning("Keyboard hook thread did not stop in time");
			}
		}
	}
}
