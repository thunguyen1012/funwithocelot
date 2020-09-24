using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Entities.Order>> GetOrdersAsync();
    }
}