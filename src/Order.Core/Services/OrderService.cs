using Common.Interfaces;
using Order.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Entities.Order>> GetOrdersAsync() => await repository.ListAsync<Entities.Order>();
    }
}