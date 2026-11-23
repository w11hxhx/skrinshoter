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
		if (window != null && window.IsLoaded)
		{
			appUpdateCheckWindowVM.Dispose();
			window.Activate();
			return Unit.Task;
		}
		(ShowAppUpdateCheckWindowCommandHandler.window = new AppUpdateCheckWindow(appUpdateCheckWindowVM)).Show();
		appUpdateCheckWindowVM.Initialize(request.Release);
		return Unit.Task;
	}
}
