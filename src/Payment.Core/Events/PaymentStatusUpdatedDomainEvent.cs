using Common;
using Common.Interfaces;
using System;

namespace Payment.Core.Events
{
    public class PaymentStatusUpdatedDomainEvent : BaseDomainEvent
    {
        public PaymentStatusUpdatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, Entities.PaymentStatus status)
            : base(aggregateRootId, version, createdDate, header)
        {
            Status = status;
        }

        public Entities.PaymentStatus Status { get; }

        public static PaymentStatusUpdatedDomainEvent Create(
            BaseAggregateRoot aggregateRoot, 
            Entities.PaymentStatus status)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new PaymentStatusUpdatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, status);

            return domainEvent;
        }
    }
}