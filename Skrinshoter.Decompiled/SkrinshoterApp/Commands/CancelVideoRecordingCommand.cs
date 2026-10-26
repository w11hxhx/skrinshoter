using MediatR;

namespace SkrinshoterApp.Commands;

public class CancelVideoRecordingCommand : IRequest<Unit>, IBaseRequest
{
}
