using Common;
using Common.Interfaces;
using MediatR;
using Order.Core.Entities;
using System;

namespace Order.Core.Events
{
    public class OrderRequestedPaymentDomainEvent : BaseDomainEvent, IRequest<Guid>
    {
        public OrderStatus Status { get; }

        public OrderRequestedPaymentDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header, OrderStatus status)
            : base(aggregateRootId, version, createdDate, header)
        {
            this.Status = status;
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
}