using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payment.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Entities.Payment>> GetPaymentsAsync();
    }
}