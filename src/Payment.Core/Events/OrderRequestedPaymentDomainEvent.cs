using Common;
using Common.Interfaces;
using MediatR;
using System;

namespace Payment.Core.Events
{
    public class OrderRequestedPaymentDomainEvent : BaseDomainEvent//, IRequest<Guid>
    {
        public OrderRequestedPaymentDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header)
            : base(aggregateRootId, version, createdDate, header)
        {
        }

        public static OrderRequestedPaymentDomainEvent Create(BaseAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new OrderRequestedPaymentDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null);

            return domainEvent;
        }
    }
}