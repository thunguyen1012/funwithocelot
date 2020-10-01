using Payment.Core.Entities;
using System;

namespace Payment.WebAPI.DTOs
{
    public class PaymentDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; }

        public static PaymentDTO FromEntity(Core.Entities.Payment source)
        {
            return new PaymentDTO
            {
                Id = source.Id,
                OrderId = source.OrderId,
                Status = source.Status
            };
        }
    }
}