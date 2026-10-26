using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MouseHook;
using SkrinshoterApp.Commands;
using Windows.Win32;

namespace SkrinshoterApp.BackgroundServices;

public class MouseHotkeyService : IHostedService
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CProcessHotkey_003Ed__13 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public MouseHotkeyService _003C_003E4__this;

		private TaskAwaiter<Unit> _003C_003Eu__1;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			MouseHotkeyService mouseHotkeyService = _003C_003E4__this;
			try
			{
				_ = 1;
				try
				{
					TaskAwaiter<Unit> awaiter;
					if (num == 0)
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default;
						num = (_003C_003E1__state = -1);
						goto IL_00c6;
					}
					if (num == 1)
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default;
						num = (_003C_003E1__state = -1);
						goto IL_015d;
					}
					if (mouseHotkeyService.settings.UseMouseForVideo && PInvoke.GetAsyncKeyState(17) < 0 && PInvoke.GetAsyncKeyState(16) < 0)
					{
						mouseHotkeyService.logger.LogInformation("Mouse hotkey pressed: video");
						awaiter = mouseHotkeyService.mediator.Send((IRequest<Unit>)new RecordVideoCommand(), default(CancellationToken)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00c6;
					}
					if (mouseHotkeyService.settings.UseMouseForScreenshot)
					{
						mouseHotkeyService.logger.LogInformation("Mouse hotkey pressed: screenshot");
						awaiter = mouseHotkeyService.mediator.Send((IRequest<Unit>)new MakeRegionScreenshotCommand(CommandInvokeType.Mouse), default(CancellationToken)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_015d;
					}
					goto end_IL_0013;
					IL_00c6:
					awaiter.GetResult();
					goto end_IL_0013;
					IL_015d:
					awaiter.GetResult();
					end_IL_0013:;
				}
				catch (Exception exception)
				{
					mouseHotkeyService.logger.LogError(exception, "Failed to process mouse hotkey");
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

	private readonly MouseHotkeyMonitor mouseHotkeyMonitor;

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly Dispatcher dispatcher;

	private readonly IMediator mediator;

	private readonly ILogger logger;

	private bool isStarted;

	[UIThread]
	public MouseHotkeyService(MouseHotkeyMonitor mouseHotkeyMonitor, SettingsManager settingsManager, Dispatcher dispatcher, IMediator mediator, ILogger<MouseHotkeyService> logger)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(null, methodFromHandle, new object[5] { mouseHotkeyMonitor, settingsManager, dispatcher, mediator, logger });
		uIThreadAttribute.OnEntry();
		try
		{
			this.mouseHotkeyMonitor = mouseHotkeyMonitor;
			this.settingsManager = settingsManager;
			settings = settingsManager.Settings;
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
			settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
			mouseHotkeyMonitor.HotkeyPressed += MouseHotkeyMonitor_HotkeyPressed;
			isStarted = true;
			UpdateState();
			return Task.CompletedTask;
		});
	}

	[AnyThread]
	public Task StopAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			settingsManager.SettingsChanged -= SettingsManager_SettingsChanged;
			mouseHotkeyMonitor.HotkeyPressed -= MouseHotkeyMonitor_HotkeyPressed;
			isStarted = false;
			UpdateState();
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
			if (e.Old.UseMouseForScreenshot != e.New.UseMouseForScreenshot || e.Old.UseMouseForVideo != e.New.UseMouseForVideo || e.Old.SilentMode != e.New.SilentMode)
			{
				UpdateState();
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
	private void MouseHotkeyMonitor_HotkeyPressed(object sender, EventArgs e)
	{
		dispatcher.InvokeIfRequiredAsync(ProcessHotkey);
	}

	[UIThread]
	private void UpdateState()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			if (isStarted && (settings.UseMouseForScreenshot || settings.UseMouseForVideo) && !settings.SilentMode)
			{
				mouseHotkeyMonitor.Start();
			}
			else
			{
				mouseHotkeyMonitor.Stop();
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[AsyncStateMachine(typeof(_003CProcessHotkey_003Ed__13))]
	[UIThread]
	private Task ProcessHotkey()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			_003CProcessHotkey_003Ed__13 stateMachine = default;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
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
}
