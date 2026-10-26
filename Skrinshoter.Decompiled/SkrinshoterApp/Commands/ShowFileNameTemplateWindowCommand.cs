using MediatR;

namespace SkrinshoterApp.Commands;

public class ShowFileNameTemplateWindowCommand : IRequest<Unit>, IBaseRequest
{
	public bool IsScreenshotTemplate { get; }

	public ShowFileNameTemplateWindowCommand(bool isScreenshotTemplate)
	{
		IsScreenshotTemplate = isScreenshotTemplate;
	}
}
