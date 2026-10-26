using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SkrinshoterApp.Commands;

public class ShowFileNameTemplateWindowCommandHandler : IRequestHandler<ShowFileNameTemplateWindowCommand, Unit>
{
	private readonly FileNameTemplateWindow fileNameTemplateWindow;

	private readonly SettingsWindow settingsWindow;

	private readonly SettingsManager settingsManager;

	public ShowFileNameTemplateWindowCommandHandler(FileNameTemplateWindow fileNameTemplateWindow, SettingsWindow settingsWindow, SettingsManager settingsManager)
	{
		this.fileNameTemplateWindow = fileNameTemplateWindow;
		this.settingsWindow = settingsWindow;
		this.settingsManager = settingsManager;
	}

	public Task<Unit> Handle(ShowFileNameTemplateWindowCommand request, CancellationToken cancellationToken)
	{
		FileNameTemplate template = (request.IsScreenshotTemplate ? settingsManager.Settings.ScreenshotFileNameTemplate : settingsManager.Settings.VideoFileNameTemplate);
		fileNameTemplateWindow.Owner = settingsWindow;
		FileNameTemplateWindowVM fileNameTemplateWindowVM = (FileNameTemplateWindowVM)fileNameTemplateWindow.DataContext;
		fileNameTemplateWindowVM.Template = template;
		fileNameTemplateWindowVM.IsScreenshotTemplate = request.IsScreenshotTemplate;
		if (fileNameTemplateWindow.ShowDialog() == true)
		{
			if (request.IsScreenshotTemplate)
			{
				settingsManager.Settings.ScreenshotFileNameTemplate = fileNameTemplateWindowVM.Template;
			}
			else
			{
				settingsManager.Settings.VideoFileNameTemplate = fileNameTemplateWindowVM.Template;
			}
			settingsManager.SaveSettings();
		}
		return Unit.Task;
	}
}
