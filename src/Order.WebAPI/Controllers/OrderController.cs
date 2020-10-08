using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Commands;
using Order.Core.Interfaces;
using Order.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IOrderService orderService;

        public OrderController(IMediator mediator, IOrderService orderService)
        {
            this.mediator = mediator;
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> ListOrders()
        {
            var orders = await orderService.GetOrdersAsync();

            var orderDTOs = orders.Aggregate(new List<OrderDTO>(), (result, order) =>
            {
                result.Add(OrderDTO.FromEntity(order));
                return result;
            });

            return Ok(orderDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel createOrderViewModel)
        {
            var id = await mediator.Send(new CreateOrderCommand(createOrderViewModel.ProductId));

            // Pay
            Pay(new RequestPaymentCommand(id));

            return Ok(id);
        }

        private void Pay(RequestPaymentCommand command)
        {
            mediator.Send(command);
        }
    }

    public class CreateOrderViewModel
    {
        public Guid ProductId { get; set; }
    }
}