using Common.Interfaces;
using System;

namespace Common.Messages.Order.Events
{
    public class OrderStatusUpdatedDomainEvent : BaseDomainEvent
    {
        public OrderStatusUpdatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, OrderStatus status)
            : base(aggregateRootId, version, createdDate, header)
        {
            Status = status;
        }

        public OrderStatus Status { get; }

        public static OrderStatusUpdatedDomainEvent Create(BaseAggregateRoot aggregateRoot, OrderStatus status)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new OrderStatusUpdatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, status);

            return domainEvent;
        }
    }
}