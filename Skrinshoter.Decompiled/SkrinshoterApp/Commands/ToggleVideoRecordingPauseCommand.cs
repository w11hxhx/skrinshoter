using MediatR;

namespace SkrinshoterApp.Commands;

public class ToggleVideoRecordingPauseCommand : IRequest<Unit>, IBaseRequest
{
}
