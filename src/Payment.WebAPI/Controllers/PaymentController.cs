using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Payment.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Payment> Get()
        {
            return new List<Payment> 
            {
                new Payment 
                {
                    Amount = 10, 
                    Date = DateTime.UtcNow
                },
                new Payment
                {
                    Amount = 20,
                    Date = DateTime.UtcNow
                },
            };
        }
    }
}