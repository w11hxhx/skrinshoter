using MediatR;

namespace SkrinshoterApp.Commands;

public class ShowSettingsWindowCommand : IRequest<Unit>, IBaseRequest
{
	public bool SwitchToAboutTab { get; }

	public ShowSettingsWindowCommand(bool switchToAboutTab = false)
	{
		SwitchToAboutTab = switchToAboutTab;
	}
}
