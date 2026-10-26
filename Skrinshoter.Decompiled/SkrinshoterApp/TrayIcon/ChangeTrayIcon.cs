using System.Drawing;
using MediatR;

namespace SkrinshoterApp.TrayIcon;

internal class ChangeTrayIcon : INotification
{
	public Icon Icon { get; }

	public ChangeTrayIcon(Icon icon)
	{
		Icon = icon;
	}
}
