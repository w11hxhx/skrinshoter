using MediatR;

namespace SkrinshoterApp.Commands;

public class ShowAppUpdateCheckWindowCommand : IRequest<Unit>, IBaseRequest
{
    public ForkRelease Release { get; set; }
}
