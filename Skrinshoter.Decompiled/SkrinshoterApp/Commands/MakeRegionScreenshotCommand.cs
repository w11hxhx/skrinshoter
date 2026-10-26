using MediatR;

namespace SkrinshoterApp.Commands;

public class MakeRegionScreenshotCommand : IRequest<Unit>, IBaseRequest
{
	public CommandInvokeType CommandInvokeType { get; }

	public MakeRegionScreenshotCommand(CommandInvokeType commandInvokeType = CommandInvokeType.Other)
	{
		CommandInvokeType = commandInvokeType;
	}
}
