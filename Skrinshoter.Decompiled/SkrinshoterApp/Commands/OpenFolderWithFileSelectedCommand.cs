using System;
using MediatR;

namespace SkrinshoterApp.Commands;

public class OpenFolderWithFileSelectedCommand : IRequest<Unit>, IBaseRequest
{
	public string Path { get; }

	public OpenFolderWithFileSelectedCommand(string path)
	{
		Path = path ?? throw new ArgumentNullException("path");
	}
}
