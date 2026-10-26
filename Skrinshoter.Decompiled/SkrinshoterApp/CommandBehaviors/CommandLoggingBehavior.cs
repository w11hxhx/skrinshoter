using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp.CommandBehaviors;

public class CommandLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
	private readonly ILogger<CommandLoggingBehavior<TRequest, TResponse>> logger;

	public CommandLoggingBehavior(ILogger<CommandLoggingBehavior<TRequest, TResponse>> logger)
	{
		this.logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		logger.LogInformation("{requestName}: Start", typeof(TRequest).Name);
		try
		{
			return await next();
		}
		finally
		{
			logger.LogInformation("{requestName}: End", typeof(TRequest).Name);
		}
	}
}
