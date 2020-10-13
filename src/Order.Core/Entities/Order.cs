using Common;
using Common.Messages.Order.Events;
using System;

namespace Order.Core.Entities
{
    public class Order : BaseAggregateRoot
    {
        public Guid ProductId { get; set; }
        public Guid PaymentId { get; set; }
        public OrderStatus Status { get; set; }

        private Order()
        {
            Register<OrderCreatedDomainEvent>(When);
            Register<OrderStatusUpdatedDomainEvent>(When);
            Register<OrderProductUpdatedDomainEvent>(When);
            Register<OrderRequestedPaymentDomainEvent>(When);
        }

        public static Order Create() => new Order();

        public void Start()
        {
            Raise(OrderCreatedDomainEvent.Create(this));
        }

        public void UpdateStatus(OrderStatus status)
        {
            Raise(OrderStatusUpdatedDomainEvent.Create(this, (Common.Messages.Order.Events.OrderStatus)status));
        }

        public void UpdateProductId(Guid productId)
        {
            Raise(OrderProductUpdatedDomainEvent.Create(this, productId));
        }

        public void RequestPayment()
        {
            Raise(OrderRequestedPaymentDomainEvent.Create(this));
        }

        protected void When(OrderCreatedDomainEvent @event)
        {
            Id = @event.AggregateRootId;
        }

        protected void When(OrderStatusUpdatedDomainEvent @event)
        {
            Status = (OrderStatus)@event.Status;
        }

        protected void When(OrderProductUpdatedDomainEvent @event)
        {
            ProductId = @event.ProductId;
        }

        protected void When(OrderRequestedPaymentDomainEvent @event)
        {
            Status = (OrderStatus)@event.Status;
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