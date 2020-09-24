using Common;
using Common.Interfaces;
using System;

namespace Order.Core.Entities
{
    public class Order : BaseEntity, IAggregateRoot, IAuditableEntity
    {
        public int ProductId { get; set; }
        public int PaymentId { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Paid,
        Cancelled
    }
}