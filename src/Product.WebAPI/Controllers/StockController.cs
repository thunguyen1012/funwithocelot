using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Stock> Get() => Program.Inventory;

        [HttpPut]
        public Task<bool> DecreaseProductQuantity([FromBody] DecreaseProductQuantityDTO request)
        {
            var product = Program.Inventory.Find(p => p.ProductId == request.ProductId);

            if (product != null && product.Quantity >= request.Quantity)
            {
                product.Quantity -= request.Quantity;
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }

    public class DecreaseProductQuantityDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}