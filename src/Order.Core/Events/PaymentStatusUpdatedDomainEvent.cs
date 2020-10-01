using Common;
using Common.Interfaces;
using System;

namespace Order.Core.Events
{
    public class PaymentStatusUpdatedDomainEvent : BaseDomainEvent
    {
        public PaymentStatusUpdatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, PaymentStatus status, Guid orderId)
            : base(aggregateRootId, version, createdDate, header)
        {
            Status = status;
            OrderId = orderId;
        }

        public PaymentStatus Status { get; }
        public Guid OrderId { get; set; }

        public static PaymentStatusUpdatedDomainEvent Create(BaseAggregateRoot aggregateRoot, PaymentStatus status, Guid orderId)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new PaymentStatusUpdatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, status, orderId);

            return domainEvent;
        }
    }

    public enum PaymentStatus
    {
        New,
        Inprocess,
        Paid,
        Failed
    }
}