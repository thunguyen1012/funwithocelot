using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IPublisher
    {
        Task Publish(BaseDomainEvent domainEvent);

        Task Publish(IEnumerable<BaseDomainEvent> domainEvents, IHeader header);
    }
}