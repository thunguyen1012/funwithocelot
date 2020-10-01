using Common.Interfaces;
using MediatR;
using Order.Core.Events;
using System;

namespace Order.Core.EventHandlers
{
    public class PaymentStatusUpdatedDomainEventHandler : RequestHandler<PaymentStatusUpdatedDomainEvent>
    {
        private readonly IRepository repository;

        public PaymentStatusUpdatedDomainEventHandler(IRepository repository)
        {
            this.repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        protected override void Handle(PaymentStatusUpdatedDomainEvent domainEvent)
        {
            var order = repository.GetByIdAsync<Entities.Order>(domainEvent.OrderId).GetAwaiter().GetResult();
            order.UpdateStatus(Entities.OrderStatus.Paid);

            order.Status = Entities.OrderStatus.Paid;
            repository.UpdateAsync(order).GetAwaiter();

            //or
            //order.Apply(domainEvent);
            //var result = repository.AddAsync(order).GetAwaiter().GetResult();
        }
    }
}