using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Commands;

namespace SkrinshoterApp;

public sealed class CommandLineActionExecutor
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CExecuteAsync_003Ed__4 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public CommandLineArguments arguments;

		public CommandLineActionExecutor _003C_003E4__this;

		private TaskAwaiter<Unit> _003C_003Eu__1;

		private TaskAwaiter _003C_003Eu__2;

		private void MoveNext()
		{
			int num = _003C_003E1__state;
			CommandLineActionExecutor commandLineActionExecutor = _003C_003E4__this;
			try
			{
				if ((uint)num <= 4u)
				{
					goto IL_0070;
				}
				if (arguments == null)
				{
					throw new ArgumentNullException("arguments");
				}
				CommandLineAction action = arguments.Action;
				if ((action != CommandLineAction.None && action != CommandLineAction.SilentRun) || 1 == 0)
				{
					commandLineActionExecutor.logger.LogInformation("Executing command line action: {action}", arguments.Action);
					goto IL_0070;
				}
				goto end_IL_000e;
				IL_0070:
				try
				{
					TaskAwaiter<Unit> awaiter2;
					TaskAwaiter awaiter;
					switch (num)
					{
					default:
						switch (arguments.Action)
						{
						case CommandLineAction.ShowSettingsWindow:
							break;
						case CommandLineAction.MakeScreenshot:
							goto IL_0135;
						case CommandLineAction.RecordVideo:
							goto IL_01ae;
						case CommandLineAction.UploadFile:
							goto IL_0226;
						case CommandLineAction.Shutdown:
							goto IL_02a9;
						default:
							commandLineActionExecutor.logger.LogWarning("Unknown command line action: {action}", arguments.Action);
							goto end_IL_0070;
						}
						awaiter2 = commandLineActionExecutor.mediator.Send((IRequest<Unit>)new ShowSettingsWindowCommand(), default(CancellationToken)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0128;
					case 0:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default;
						num = (_003C_003E1__state = -1);
						goto IL_0128;
					case 1:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default;
						num = (_003C_003E1__state = -1);
						goto IL_01a1;
					case 2:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default;
						num = (_003C_003E1__state = -1);
						goto IL_0219;
					case 3:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default;
						num = (_003C_003E1__state = -1);
						goto IL_029c;
					case 4:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default;
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_02a9:
						awaiter = commandLineActionExecutor.appShutdownService.ShutdownAsync().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_01a1:
						awaiter2.GetResult();
						goto end_IL_0070;
						IL_0226:
						awaiter2 = commandLineActionExecutor.mediator.Send((IRequest<Unit>)new UploadFileToCloudCommand(arguments.Argument), default(CancellationToken)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_029c;
						IL_0135:
						awaiter2 = commandLineActionExecutor.mediator.Send((IRequest<Unit>)new MakeRegionScreenshotCommand(), default(CancellationToken)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_01a1;
						IL_029c:
						awaiter2.GetResult();
						goto end_IL_0070;
						IL_01ae:
						awaiter2 = commandLineActionExecutor.mediator.Send((IRequest<Unit>)new RecordVideoCommand(), default(CancellationToken)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0219;
						IL_0128:
						awaiter2.GetResult();
						goto end_IL_0070;
						IL_0219:
						awaiter2.GetResult();
						goto end_IL_0070;
					}
					awaiter.GetResult();
					end_IL_0070:;
				}
				catch (Exception exception)
				{
					commandLineActionExecutor.logger.LogError(exception, "Failed to execute command line action: {action}", arguments.Action);
				}
				end_IL_000e:;
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

	private readonly IMediator mediator;

	private readonly AppShutdownService appShutdownService;

	private readonly ILogger logger;

	public CommandLineActionExecutor(IMediator mediator, AppShutdownService appShutdownService, ILogger<CommandLineActionExecutor> logger)
	{
		this.mediator = mediator ?? throw new ArgumentNullException("mediator");
		this.appShutdownService = appShutdownService ?? throw new ArgumentNullException("appShutdownService");
		this.logger = logger ?? throw new ArgumentNullException("logger");
	}

	[AsyncStateMachine(typeof(_003CExecuteAsync_003Ed__4))]
	[UIThread]
	public Task ExecuteAsync(CommandLineArguments arguments)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[1] { arguments });
		uIThreadAttribute.OnEntry();
		try
		{
			_003CExecuteAsync_003Ed__4 stateMachine = default;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.arguments = arguments;
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
