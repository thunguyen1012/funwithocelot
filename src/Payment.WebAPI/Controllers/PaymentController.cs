using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.Core.Interfaces;
using Payment.WebAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPaymentService paymentService;

        public PaymentController(IMediator mediator, IPaymentService paymentService)
        {
            this.mediator = mediator;
            this.paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var payments = await paymentService.GetPaymentsAsync();

            var paymentDTOs = payments.Aggregate(new List<PaymentDTO>(), (result, item) =>
            {
                result.Add(PaymentDTO.FromEntity(item));
                return result;
            });

            return Ok(paymentDTOs);
        }
    }
}