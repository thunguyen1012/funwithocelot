using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Product.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> Get() => new List<Product>
            {
                new Product { Name = "P1"},
                new Product { Name = "P2"},
                new Product { Name = "P3"}
            };
    }
}