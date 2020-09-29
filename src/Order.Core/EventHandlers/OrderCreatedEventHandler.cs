using Common.Interfaces;
using MediatR;
using Order.Core.Events;
using System;

namespace Order.Core.EventHandlers
{
    public class OrderCreatedEventHandler : RequestHandler<OrderCreatedDomainEvent, Guid>
    {
        private readonly IRepository repository;

        public OrderCreatedEventHandler(IRepository repository)
        {
            this.repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        protected override Guid Handle(OrderCreatedDomainEvent domainEvent)
        {
            var order = Entities.Order.Create();
            order.Apply(domainEvent);
            var result = repository.AddAsync(order).GetAwaiter().GetResult();

            return result.Id;
        }
    }
}