using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MediatR;

namespace SkrinshoterApp.Commands;

public class ShowAppUpdateCheckWindowCommandHandler : IRequestHandler<ShowAppUpdateCheckWindowCommand, Unit>
{
	private readonly AppUpdateCheckWindowVM appUpdateCheckWindowVM;

	private static Window window;

	public ShowAppUpdateCheckWindowCommandHandler(AppUpdateCheckWindowVM appUpdateCheckWindowVM)
	{
		this.appUpdateCheckWindowVM = appUpdateCheckWindowVM;
	}

	public Task<Unit> Handle(ShowAppUpdateCheckWindowCommand request, CancellationToken cancellationToken)
	{
		Window window = ShowAppUpdateCheckWindowCommandHandler.window;
		if (window != null)
		{
			// A manual check must fetch fresh release metadata. Keep an active
			// download and avoid replacing an open window for background offers.
			if (request.Release != null || window.DataContext is AppUpdateCheckWindowVM { Busy: true })
			{
				appUpdateCheckWindowVM.Dispose();
				if (window.WindowState == WindowState.Minimized)
					window.WindowState = WindowState.Normal;
				window.Activate();
				return Unit.Task;
			}
			window.Close();
		}
		var opened = new AppUpdateCheckWindow(appUpdateCheckWindowVM);
		ShowAppUpdateCheckWindowCommandHandler.window = opened;
		opened.Closed += (_, __) => ShowAppUpdateCheckWindowCommandHandler.window = null;
		opened.Show();
		opened.Activate();
		appUpdateCheckWindowVM.Initialize(request.Release);
		return Unit.Task;
	}
}
