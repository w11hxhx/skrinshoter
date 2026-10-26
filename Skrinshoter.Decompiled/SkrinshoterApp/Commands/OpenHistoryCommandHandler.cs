using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.Commands;

public class OpenHistoryCommandHandler : IRequestHandler<OpenHistoryCommand, Unit>
{
	private readonly AppInfoProvider appInfoProvider;

	private readonly IMediator mediator;

	public OpenHistoryCommandHandler(AppInfoProvider appInfoProvider, IMediator mediator)
	{
		this.appInfoProvider = appInfoProvider;
		this.mediator = mediator;
	}

	public Task<Unit> Handle(OpenHistoryCommand request, CancellationToken cancellationToken)
	{
		Uri url = new Uri(new Uri(ProductUrl.Resolve(ProductResources.BaseUrl_MainApi)), "my?appId=" + appInfoProvider.AppId);
		return mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(url), default(CancellationToken));
	}
}
