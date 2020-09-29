using Order.Core.Entities;
using System;

namespace Order.WebAPI.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid PaymentId { get; set; }
        public OrderStatus Status { get; set; }

        public static OrderDTO FromEntity(Core.Entities.Order source)
        {
            return new OrderDTO
            {
                Id = source.Id,
                ProductId = source.ProductId,
                PaymentId = source.PaymentId,
                Status = source.Status
            };
        }
    }
}