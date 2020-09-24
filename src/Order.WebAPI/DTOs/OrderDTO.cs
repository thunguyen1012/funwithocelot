using Order.Core.Entities;

namespace Order.WebAPI.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PaymentId { get; set; }
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