using MediatR;

namespace SkrinshoterApp.Commands;

public class UploadFileToCloudCommand : IRequest<Unit>, IBaseRequest
{
	public string FilePath { get; }

	public UploadFileToCloudCommand(string filePath = null)
	{
		FilePath = filePath;
	}
}
