using System;
using Hardcodet.Wpf.TaskbarNotification;
using MediatR;

namespace SkrinshoterApp.Notifications;

internal class ShowNotification : INotification
{
	public string Text { get; }

	public Action<NotificationOptions> OptionsBuilder { get; }

	public BalloonIcon Icon { get; }

	public ShowNotification(string text, Action<NotificationOptions> optionsBuilder, BalloonIcon icon)
	{
		Text = text;
		OptionsBuilder = optionsBuilder;
		Icon = icon;
	}
}
