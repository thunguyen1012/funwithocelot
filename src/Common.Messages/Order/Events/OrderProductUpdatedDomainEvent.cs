using Common.Interfaces;
using System;

namespace Common.Messages.Order.Events
{
    public class OrderProductUpdatedDomainEvent : BaseDomainEvent
    {
        public OrderProductUpdatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, Guid productId)
            : base(aggregateRootId, version, createdDate, header)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; }

        public static OrderProductUpdatedDomainEvent Create(BaseAggregateRoot aggregateRoot, Guid productId)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new OrderProductUpdatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, productId);

            return domainEvent;
        }
    }
}