using Common.Interfaces;
using Payment.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payment.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository repository;

        public PaymentService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Entities.Payment>> GetPaymentsAsync() => await repository.ListAsync<Entities.Payment>();
    }
}