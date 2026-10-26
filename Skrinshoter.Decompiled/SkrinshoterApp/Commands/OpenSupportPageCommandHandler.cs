using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.Commands;

public class OpenSupportPageCommandHandler : IRequestHandler<OpenSupportPageCommand, Unit>
{
	private readonly IMediator mediator;

	private readonly AppInfoProvider appInfoProvider;

	public OpenSupportPageCommandHandler(IMediator mediator, AppInfoProvider appInfoProvider)
	{
		this.mediator = mediator;
		this.appInfoProvider = appInfoProvider;
	}

	public Task<Unit> Handle(OpenSupportPageCommand request, CancellationToken cancellationToken)
	{
		return mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand($"{ProductUrl.Resolve(ProductResources.CustomerSupportUrl)}?product={ProductResources.ProductId}&version={appInfoProvider.Version}"), default(CancellationToken));
	}
}
