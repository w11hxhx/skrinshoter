using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using KeyboardHook;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Commands;

namespace SkrinshoterApp.BackgroundServices;

public class KeyboardHotkeyService : IHostedService
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CProcessHotkey_003Ed__27 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public int hotkeyId;

		public KeyboardHotkeyService _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			KeyboardHotkeyService keyboardHotkeyService = _003C_003E4__this;
			try
			{
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = (hotkeyId switch
						{
							1 => (Func<Task>)keyboardHotkeyService.AreaScreenshotHotkeyHandler, 
							2 => keyboardHotkeyService.ActiveWindowScreenshotHotkeyHandler, 
							3 => keyboardHotkeyService.ActiveMonitorScreenshotHotkeyHandler, 
							4 => keyboardHotkeyService.DesktopScreenshotHotkeyHandler, 
							5 => keyboardHotkeyService.RecordVideoHotkeyHandler, 
							6 => keyboardHotkeyService.PauseVideoHotkeyHandler, 
							_ => throw new InvalidOperationException($"Unknown hotkey id: {hotkeyId}"), 
						})().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default;
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					keyboardHotkeyService.logger.LogError(exception, "Failed to process hotkey id: {hotkeyId}", hotkeyId);
				}
			}
			catch (Exception exception2)
			{
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetException(exception2);
				return;
			}
			_003C_003E1__state = -2;
			_003C_003Et__builder.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			_003C_003Et__builder.SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	private const int AreaScreenshotHotkeyId = 1;

	private const int ActiveWindowScreenshotHotkeyId = 2;

	private const int ActiveMonitorScreenshotHotkeyId = 3;

	private const int DesktopScreenshotHotkeyId = 4;

	private const int RecordVideoHotkeyId = 5;

	private const int PauseVideoHotkeyId = 6;

	private readonly KeyboardHotkeyMonitor keyboardHotkeyMonitor;

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly IOperationStateProvider operationStateProvider;

	private readonly Dispatcher dispatcher;

	private readonly IMediator mediator;

	private readonly ILogger logger;

	private bool isStarted;

	[UIThread]
	public KeyboardHotkeyService(KeyboardHotkeyMonitor keyboardHotkeyMonitor, SettingsManager settingsManager, IOperationStateProvider operationStateProvider, Dispatcher dispatcher, IMediator mediator, ILogger<KeyboardHotkeyService> logger)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(null, methodFromHandle, new object[6] { keyboardHotkeyMonitor, settingsManager, operationStateProvider, dispatcher, mediator, logger });
		uIThreadAttribute.OnEntry();
		try
		{
			this.keyboardHotkeyMonitor = keyboardHotkeyMonitor;
			this.settingsManager = settingsManager;
			settings = settingsManager.Settings;
			this.operationStateProvider = operationStateProvider;
			this.dispatcher = dispatcher;
			this.mediator = mediator;
			this.logger = logger;
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[AnyThread]
	public Task StartAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			RegisterHotkey(settings.AreaScreenshotHotkey, 1);
			RegisterHotkey(settings.ActiveWindowScreenshotHotkey, 2);
			RegisterHotkey(settings.ActiveMonitorScreenshotHotkey, 3);
			RegisterHotkey(settings.DesktopScreenshotHotkey, 4);
			RegisterHotkey(settings.RecordVideoHotkey, 5);
			RegisterHotkey(settings.PauseVideoHotkey, 6);
			settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
			keyboardHotkeyMonitor.HotkeyPressed += KeyboardHotkeyMonitor_HotkeyPressed;
			isStarted = true;
			return Task.CompletedTask;
		});
	}

	[AnyThread]
	public Task StopAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			settingsManager.SettingsChanged -= SettingsManager_SettingsChanged;
			keyboardHotkeyMonitor.HotkeyPressed -= KeyboardHotkeyMonitor_HotkeyPressed;
			UnregisterHotkey(settings.AreaScreenshotHotkey);
			UnregisterHotkey(settings.ActiveWindowScreenshotHotkey);
			UnregisterHotkey(settings.ActiveMonitorScreenshotHotkey);
			UnregisterHotkey(settings.DesktopScreenshotHotkey);
			UnregisterHotkey(settings.RecordVideoHotkey);
			UnregisterHotkey(settings.PauseVideoHotkey);
			isStarted = false;
			return Task.CompletedTask;
		});
	}

	[UIThread]
	private void SettingsManager_SettingsChanged(object sender, SettingsChangedEventArgs e)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { sender, e });
		uIThreadAttribute.OnEntry();
		try
		{
			if (isStarted)
			{
				if (e.Old.AreaScreenshotHotkey != e.New.AreaScreenshotHotkey)
				{
					UnregisterHotkey(e.Old.AreaScreenshotHotkey);
					RegisterHotkey(e.New.AreaScreenshotHotkey, 1);
				}
				if (e.Old.ActiveWindowScreenshotHotkey != e.New.ActiveWindowScreenshotHotkey)
				{
					UnregisterHotkey(e.Old.ActiveWindowScreenshotHotkey);
					RegisterHotkey(e.New.ActiveWindowScreenshotHotkey, 2);
				}
				if (e.Old.ActiveMonitorScreenshotHotkey != e.New.ActiveMonitorScreenshotHotkey)
				{
					UnregisterHotkey(e.Old.ActiveMonitorScreenshotHotkey);
					RegisterHotkey(e.New.ActiveMonitorScreenshotHotkey, 3);
				}
				if (e.Old.DesktopScreenshotHotkey != e.New.DesktopScreenshotHotkey)
				{
					UnregisterHotkey(e.Old.DesktopScreenshotHotkey);
					RegisterHotkey(e.New.DesktopScreenshotHotkey, 4);
				}
				if (e.Old.RecordVideoHotkey != e.New.RecordVideoHotkey)
				{
					UnregisterHotkey(e.Old.RecordVideoHotkey);
					RegisterHotkey(e.New.RecordVideoHotkey, 5);
				}
				if (e.Old.PauseVideoHotkey != e.New.PauseVideoHotkey)
				{
					UnregisterHotkey(e.Old.PauseVideoHotkey);
					RegisterHotkey(e.New.PauseVideoHotkey, 6);
				}
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[AnyThread]
	private void KeyboardHotkeyMonitor_HotkeyPressed(object sender, KeyboardHotkeyPressedEventArgs e)
	{
		dispatcher.InvokeIfRequiredAsync(() => ProcessHotkey(e.HotkeyId));
	}

	private Task AreaScreenshotHotkeyHandler()
	{
		logger.LogInformation("Hotkey: On region screenshot");
		return mediator.Send((IRequest<Unit>)new MakeRegionScreenshotCommand(CommandInvokeType.Hotkey), default(CancellationToken));
	}

	private Task ActiveWindowScreenshotHotkeyHandler()
	{
		logger.LogInformation("Hotkey: On active window screenshot");
		return mediator.Send((IRequest<Unit>)new MakeActiveWindowScreenshotCommand(), default(CancellationToken));
	}

	private Task ActiveMonitorScreenshotHotkeyHandler()
	{
		logger.LogInformation("Hotkey: On active monitor screenshot");
		return mediator.Send((IRequest<Unit>)new MakeActiveMonitorScreenshotCommand(), default(CancellationToken));
	}

	private Task DesktopScreenshotHotkeyHandler()
	{
		logger.LogInformation("Hotkey: On desktop screenshot");
		return mediator.Send((IRequest<Unit>)new MakeDesktopScreenshotCommand(), default(CancellationToken));
	}

	private Task RecordVideoHotkeyHandler()
	{
		if (operationStateProvider.CurrentOperation != OperationType.None)
		{
			if (operationStateProvider.IsRecording)
			{
				logger.LogInformation("Hotkey: On stop video");
				return mediator.Send((IRequest<Unit>)new StopVideoRecordingCommand(), default(CancellationToken));
			}
			return Task.CompletedTask;
		}
		logger.LogInformation("Hotkey: On start record video");
		return mediator.Send((IRequest<Unit>)new RecordVideoCommand(), default(CancellationToken));
	}

	private Task PauseVideoHotkeyHandler()
	{
		if (operationStateProvider.IsRecording)
		{
			logger.LogInformation("Hotkey: On pause video");
			return mediator.Send((IRequest<Unit>)new ToggleVideoRecordingPauseCommand(), default(CancellationToken));
		}
		return Task.CompletedTask;
	}

	private void RegisterHotkey(Hotkey hotkey, int hotkeyId)
	{
		try
		{
			if (!hotkey.IsEmpty)
			{
				keyboardHotkeyMonitor.RegisterHotkey(KeyInterop.VirtualKeyFromKey(hotkey.Key), ConvertModifiers(hotkey.Modifiers), hotkeyId);
			}
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to register hotkey {hotkey}", hotkey);
		}
	}

	private void UnregisterHotkey(Hotkey hotkey)
	{
		try
		{
			if (!hotkey.IsEmpty)
			{
				keyboardHotkeyMonitor.UnregisterHotkey(KeyInterop.VirtualKeyFromKey(hotkey.Key), ConvertModifiers(hotkey.Modifiers));
			}
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to unregister hotkey {hotkey}", hotkey);
		}
	}

	[AsyncStateMachine(typeof(_003CProcessHotkey_003Ed__27))]
	[UIThread]
	private Task ProcessHotkey(int hotkeyId)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[1] { hotkeyId });
		uIThreadAttribute.OnEntry();
		try
		{
			_003CProcessHotkey_003Ed__27 stateMachine = default;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.hotkeyId = hotkeyId;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			Task result = stateMachine._003C_003Et__builder.Task;
			uIThreadAttribute.OnExit();
			return result;
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	private static KeyModifiers ConvertModifiers(ModifierKeys modifiers)
	{
		KeyModifiers keyModifiers = KeyModifiers.None;
		if (modifiers.HasFlag(ModifierKeys.Alt))
		{
			keyModifiers |= KeyModifiers.Alt;
		}
		if (modifiers.HasFlag(ModifierKeys.Control))
		{
			keyModifiers |= KeyModifiers.Control;
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
}
