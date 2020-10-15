using MediatR;
using System.Threading;

namespace Common.Interfaces
{
    public interface ISubscriber
    {
        void Listen(IMediator mediator, CancellationToken cancellationToken);
    }
}