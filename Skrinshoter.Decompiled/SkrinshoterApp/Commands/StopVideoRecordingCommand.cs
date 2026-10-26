using MediatR;

namespace SkrinshoterApp.Commands;

public class StopVideoRecordingCommand : IRequest<Unit>, IBaseRequest
{
}
