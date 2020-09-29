﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Commands;
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
        private readonly IMediator mediator;
        private readonly IOrderService orderService;

        public OrderController(IMediator mediator, IOrderService orderService)
        {
            this.mediator = mediator;
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

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);

            //return CreatedAtRoute("GetBlog", new { id = id }, id);
        }
    }
}