using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MediatR;

namespace SkrinshoterApp.Commands;

public class ShowSettingsWindowCommandHandler : IRequestHandler<ShowSettingsWindowCommand, Unit>
{
	private readonly SettingsWindow settingsWindow;

	public ShowSettingsWindowCommandHandler(SettingsWindow settingsWindow)
	{
		this.settingsWindow = settingsWindow;
	}

	public Task<Unit> Handle(ShowSettingsWindowCommand request, CancellationToken cancellationToken)
	{
		if (!settingsWindow.IsVisible)
		{
			settingsWindow.Show();
		}
		if (settingsWindow.WindowState == WindowState.Minimized)
		{
			settingsWindow.WindowState = WindowState.Normal;
		}
		settingsWindow.Activate();
		if (request.SwitchToAboutTab)
		{
			(settingsWindow.DataContext as SettingsWindowVM).SwitchToAboutTabCommand.ExecuteIfCan();
		}
		return Unit.Task;
	}
}
