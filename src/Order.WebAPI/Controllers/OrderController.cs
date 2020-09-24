using Microsoft.AspNetCore.Mvc;
using Order.Core.Interfaces;
using Order.WebAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await orderService.GetOrdersAsync();

            var orderDTOs = orders.Aggregate(new List<OrderDTO>(), (result, order) =>
            {
                result.Add(OrderDTO.FromEntity(order));
                return result;
            });

            return Ok(orderDTOs);
        }
    }
}