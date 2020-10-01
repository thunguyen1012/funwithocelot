using Common;
using Common.Interfaces;
using MediatR;
using System;

namespace Payment.Core.Events
{
    public class PaymentCreatedDomainEvent : BaseDomainEvent, IRequest<Guid>
    {
        public PaymentCreatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header)
            : base(aggregateRootId, version, createdDate, header)
        {
        }

        public static PaymentCreatedDomainEvent Create(BaseAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new PaymentCreatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null);

            return domainEvent;
        }
    }
}