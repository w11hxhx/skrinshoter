using System;
using System.Drawing;
using MediatR;
using SkrinshoterApp.Properties;
using SkrinshoterApp.TrayIcon;

namespace SkrinshoterApp;

public class TrayIconService
{
	private readonly IPublisher publisher;

	private Icon _currentIcon = ProductResources.AppIcon;

	public Icon CurrentIcon => _currentIcon;

	public TrayIconService(IPublisher publisher)
	{
		this.publisher = publisher;
	}

	public void ChangeIcon(Icon icon)
	{
		_currentIcon = icon ?? throw new ArgumentNullException("icon");
		publisher.Publish(new ChangeTrayIcon(icon)).GetAwaiter().GetResult();
	}
}
