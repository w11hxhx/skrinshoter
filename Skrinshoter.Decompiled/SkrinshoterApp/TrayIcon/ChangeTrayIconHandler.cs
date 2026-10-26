using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SkrinshoterApp.TrayIcon;

internal class ChangeTrayIconHandler : INotificationHandler<ChangeTrayIcon>
{
	private readonly NotifyIcon notifyIcon;

	public ChangeTrayIconHandler(NotifyIcon notifyIcon)
	{
		this.notifyIcon = notifyIcon;
	}

	public Task Handle(ChangeTrayIcon notification, CancellationToken cancellationToken)
	{
		notifyIcon.Icon = notification.Icon;
		return Task.CompletedTask;
	}
}
