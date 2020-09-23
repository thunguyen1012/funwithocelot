using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Order.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return new List<Order>
            {
                new Order
                {
                    PaymentId = 10,
                    ProductId = 20,
                    Status = OrderStatus.New
                }
            };
        }
    }
}