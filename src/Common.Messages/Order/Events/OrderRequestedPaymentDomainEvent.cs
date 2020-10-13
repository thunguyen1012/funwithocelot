using Common.Interfaces;
using System;

namespace Common.Messages.Order.Events
{
    public class OrderRequestedPaymentDomainEvent : BaseDomainEvent
    {
        public OrderStatus Status { get; }

        public OrderRequestedPaymentDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, OrderStatus status)
            : base(aggregateRootId, version, createdDate, header)
        {
            Status = status;
        }

        public static OrderRequestedPaymentDomainEvent Create(BaseAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new OrderRequestedPaymentDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, OrderStatus.RequestedPayment);

            return domainEvent;
        }
    }

    public enum OrderStatus
    {
        New,
        RequestedPayment,
        Paid,
        Cancelled
    }
}