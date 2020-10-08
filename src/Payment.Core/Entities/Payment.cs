using Common;
using Common.Interfaces;
using Payment.Core.Events;
using System;

namespace Payment.Core.Entities
{
    public class Payment : BaseAggregateRoot, IAuditableEntity
    {
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }

        private Payment()
        {
            Register<PaymentCreatedDomainEvent>(When);
            Register<PaymentOrderUpdatedDomainEvent>(When);
            Register<PaymentStatusUpdatedDomainEvent>(When);
        }

        public static Payment Create() => new Payment();

        public void Start()
        {
            Raise(PaymentCreatedDomainEvent.Create(this));
        }

        public void UpdateOrderId(Guid orderId)
        {
            Raise(PaymentOrderUpdatedDomainEvent.Create(this, orderId));
        }

        public void UpdateStatus(PaymentStatus status)
        {
            Raise(PaymentStatusUpdatedDomainEvent.Create(this, status));
        }

        protected void When(PaymentCreatedDomainEvent @event)
        {
            Id = @event.AggregateRootId;
        }

        protected void When(PaymentOrderUpdatedDomainEvent @event)
        {
            OrderId = @event.OrderId;
        }

        protected void When(PaymentStatusUpdatedDomainEvent @event)
        {
            Status = @event.Status;
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