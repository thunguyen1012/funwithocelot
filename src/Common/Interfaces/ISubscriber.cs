using MediatR;

namespace Common.Interfaces
{
    public interface ISubscriber
    {
        void Listen(IMediator mediator);
    }
}