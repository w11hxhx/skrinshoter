using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using Hardcodet.Wpf.TaskbarNotification;
using MediatR;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Notifications;

namespace SkrinshoterApp;

public class NotificationService
{
	private readonly IPublisher publisher;

	private readonly ILogger logger;

	private readonly Channel<ShowNotification> notificationQueue;

	public NotificationService(IPublisher publisher, ILogger<NotificationService> logger)
	{
		this.publisher = publisher;
		this.logger = logger;
		notificationQueue = Channel.CreateUnbounded<ShowNotification>(new UnboundedChannelOptions
		{
			SingleReader = true,
			SingleWriter = false,
			AllowSynchronousContinuations = false
		});
		Task.Run((Func<Task>)ProcessQueueAsync);
	}

	public void ShowMessage(string text, Action<NotificationOptions> optionsBuilder = null)
	{
		notificationQueue.Writer.TryWrite(new ShowNotification(text, optionsBuilder, BalloonIcon.Info));
	}

	public void ShowError(string text, Action<NotificationOptions> optionsBuilder = null)
	{
		notificationQueue.Writer.TryWrite(new ShowNotification(text, optionsBuilder, BalloonIcon.Error));
	}

	private async Task ProcessQueueAsync()
	{
		while (await notificationQueue.Reader.WaitToReadAsync().ConfigureAwait(continueOnCapturedContext: false))
		{
			ShowNotification item;
			while (notificationQueue.Reader.TryRead(out item))
			{
				try
				{
					await publisher.Publish(item).ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (TimeoutException exception)
				{
					logger.LogError(exception, "Notification completion timeout");
				}
				catch (Exception exception2)
				{
					logger.LogError(exception2, "Failed to show notification");
				}
			}
		}
	}
}
