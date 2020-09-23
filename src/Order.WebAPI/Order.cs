using System;

namespace Order.WebAPI
{
    public class Order
    {
        public int ProductId { get; set; }
        public int PaymentId { get; set; }

        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Paid,
        Cancelled
    }
}
