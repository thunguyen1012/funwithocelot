using Common;
using Common.Interfaces;
using Order.Core.Events;
using System;

namespace Order.Core.Entities
{
    public class Order : BaseAggregateRoot, IAuditableEntity
    {
        public Guid ProductId { get; set; }
        public Guid PaymentId { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }

        private Order()
        {
            Register<OrderCreatedDomainEvent>(When);
            Register<OrderStatusUpdatedDomainEvent>(When);
            Register<OrderProductUpdatedDomainEvent>(When);
        }

        public static Order Create() => new Order();

        public void Start()
        {
            Raise(OrderCreatedDomainEvent.Create(this));
        }

        public void UpdateStatus(OrderStatus status)
        {
            Raise(OrderStatusUpdatedDomainEvent.Create(this, status));
        }

        public void UpdateProductId(Guid productId)
        {
            Raise(OrderProductUpdatedDomainEvent.Create(this, productId));
        }

        protected void When(OrderCreatedDomainEvent @event)
        {
            Id = @event.AggregateRootId;
        }

        protected void When(OrderStatusUpdatedDomainEvent @event)
        {
            Status = @event.Status;
        }

        protected void When(OrderProductUpdatedDomainEvent @event)
        {
            ProductId = @event.ProductId;
        }
    }

    public enum OrderStatus
    {
        New,
        Paid,
        Cancelled
    }
}