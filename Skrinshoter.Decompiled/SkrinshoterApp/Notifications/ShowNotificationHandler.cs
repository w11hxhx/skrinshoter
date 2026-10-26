using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MediatR;
using Nito.AsyncEx;

namespace SkrinshoterApp.Notifications;

internal class ShowNotificationHandler : INotificationHandler<ShowNotification>
{
	private static readonly TimeSpan NotificationTimeout = TimeSpan.FromSeconds(30.0);

	private readonly NotifyIcon notifyIcon;

	private readonly Settings settings;

	private readonly Dispatcher dispatcher;

	private readonly IMediator mediator;

	private TaskCompletionSource<bool> currentNotificationCompletionSource;

	public ShowNotificationHandler(NotifyIcon notifyIcon, Settings settings, Dispatcher dispatcher, IMediator mediator)
	{
		this.notifyIcon = notifyIcon;
		this.settings = settings;
		this.dispatcher = dispatcher;
		this.mediator = mediator;
	}

	public async Task Handle(ShowNotification notification, CancellationToken cancellationToken)
	{
		NotificationOptions options = new NotificationOptions();
		notification.OptionsBuilder?.Invoke(options);
		if (dispatcher.InvokeIfRequired(() => settings.SilentMode) && options.RespectSilentMode)
		{
			return;
		}
		await Task.Delay(TimeSpan.FromMilliseconds(500.0), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		Task task = await dispatcher.InvokeAsync(() => ShowBalloonTip(notification, options)).Task.ConfigureAwait(continueOnCapturedContext: false);
		using CancellationTokenSource timeoutCancellationTokenSource = new CancellationTokenSource(NotificationTimeout);
		using CancellationTokenSource linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCancellationTokenSource.Token);
		int num = 0;
		try
		{
			await task.WaitAsync(linkedCancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
		}
		catch (OperationCanceledException) when (timeoutCancellationTokenSource.IsCancellationRequested)
		{
			num = 1;
		}
		if (num == 1)
		{
			await dispatcher.InvokeAsync(CompleteCurrentNotification).Task.ConfigureAwait(continueOnCapturedContext: false);
			throw new TimeoutException("Notification completion event was not received.");
		}
	}

	[UIThread]
	private void NotifyIcon_TrayBalloonTipCompleted(object sender, RoutedEventArgs e)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { sender, e });
		uIThreadAttribute.OnEntry();
		try
		{
			CompleteCurrentNotification();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private Task ShowBalloonTip(ShowNotification notification, NotificationOptions options)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { notification, options });
		uIThreadAttribute.OnEntry();
		try
		{
			NotificationOptions options2 = options;
			if (currentNotificationCompletionSource != null)
			{
				throw new InvalidOperationException("Another notification is already displayed.");
			}
			currentNotificationCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
			Task task;
			try
			{
				notifyIcon.TrayBalloonTipClosed += NotifyIcon_TrayBalloonTipCompleted;
				notifyIcon.TrayBalloonTipClicked += NotifyIcon_TrayBalloonTipCompleted;
				notifyIcon.Tag = ((options2.Command != null) ? ((Action)(() =>
				{
					mediator.Send((object)options2.Command, default(CancellationToken));
				})) : null);
				notifyIcon.ShowBalloonTip("", notification.Text, notification.Icon);
				task = currentNotificationCompletionSource.Task;
			}
			catch
			{
				notifyIcon.TrayBalloonTipClosed -= NotifyIcon_TrayBalloonTipCompleted;
				notifyIcon.TrayBalloonTipClicked -= NotifyIcon_TrayBalloonTipCompleted;
				currentNotificationCompletionSource = null;
				throw;
			}
			Task result = task;
			uIThreadAttribute.OnExit();
			return result;
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private void CompleteCurrentNotification()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			TaskCompletionSource<bool> taskCompletionSource = currentNotificationCompletionSource;
			if (taskCompletionSource != null)
			{
				notifyIcon.TrayBalloonTipClosed -= NotifyIcon_TrayBalloonTipCompleted;
				notifyIcon.TrayBalloonTipClicked -= NotifyIcon_TrayBalloonTipCompleted;
				currentNotificationCompletionSource = null;
				taskCompletionSource.TrySetResult(result: true);
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}
}
