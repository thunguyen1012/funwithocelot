using Common;
using Common.Interfaces;
using System;

namespace Common.Messages.Payment.Events
{
    public class PaymentOrderUpdatedDomainEvent : BaseDomainEvent
    {
        public PaymentOrderUpdatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, Guid orderId)
            : base(aggregateRootId, version, createdDate, header)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }

        public static PaymentOrderUpdatedDomainEvent Create(BaseAggregateRoot aggregateRoot, Guid orderId)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new PaymentOrderUpdatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, orderId);

            return domainEvent;
        }
    }
}