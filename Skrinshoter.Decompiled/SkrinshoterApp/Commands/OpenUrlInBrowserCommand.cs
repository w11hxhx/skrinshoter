using System;
using MediatR;

namespace SkrinshoterApp.Commands;

public class OpenUrlInBrowserCommand : IRequest<Unit>, IBaseRequest
{
	public string Url { get; }

	public OpenUrlInBrowserCommand(string url)
	{
		Url = url ?? throw new ArgumentNullException("url");
	}

	public OpenUrlInBrowserCommand(Uri url)
	{
		Url = url?.ToString() ?? throw new ArgumentNullException("url");
	}
}
