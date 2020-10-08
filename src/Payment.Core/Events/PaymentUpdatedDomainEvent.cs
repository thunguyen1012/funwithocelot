using Common;
using Common.Interfaces;
using Payment.Core.Entities;
using System;

namespace Payment.Core.Events
{
    public class PaymentUpdatedDomainEvent : BaseDomainEvent
    {
        public PaymentUpdatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, PaymentStatus status, Guid orderId)
            : base(aggregateRootId, version, createdDate, header)
        {
            Status = status;
            OrderId = orderId;
        }

        public PaymentStatus Status { get; }
        public Guid OrderId { get; set; }

        public static PaymentUpdatedDomainEvent Create(BaseAggregateRoot aggregateRoot, PaymentStatus status, Guid orderId)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new PaymentUpdatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, status, orderId);

            return domainEvent;
        }
    }
}