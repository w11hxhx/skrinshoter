using System.Threading;
using System.Threading.Tasks;
using Common;
using MediatR;
using SkrinshoterApp.Commands;

namespace SkrinshoterApp.CommandBehaviors;

public class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
	private readonly IOperationStateProvider operationStateProvider;

	public CommandValidationBehavior(IOperationStateProvider operationStateProvider)
	{
		this.operationStateProvider = operationStateProvider;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if (typeof(TRequest).In(typeof(MakeRegionScreenshotCommand), typeof(MakeActiveMonitorScreenshotCommand), typeof(MakeActiveWindowScreenshotCommand), typeof(MakeDesktopScreenshotCommand), typeof(MakeScrollingCaptureCommand), typeof(RecordVideoCommand)) && operationStateProvider.CurrentOperation != OperationType.None)
		{
			return default;
		}
		return await next();
	}
}
