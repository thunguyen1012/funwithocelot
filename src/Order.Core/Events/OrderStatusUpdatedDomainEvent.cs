using Common;
using Common.Interfaces;
using Order.Core.Entities;
using System;

namespace Order.Core.Events
{
    public class OrderStatusUpdatedDomainEvent : BaseDomainEvent
    {
        public OrderStatusUpdatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, Order.Core.Entities.OrderStatus status)
            : base(aggregateRootId, version, createdDate, header)
        {
            Status = status;
        }

        public OrderStatus Status { get; }

        public static OrderStatusUpdatedDomainEvent Create(BaseAggregateRoot aggregateRoot, Entities.OrderStatus status)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new OrderStatusUpdatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, status);

            return domainEvent;
        }
    }
}