using Common.Interfaces;
using MediatR;
using Order.Core.Events;
using System;

namespace Order.Core.EventHandlers
{
    public class PaymentStatusUpdatedDomainEventHandler : RequestHandler<PaymentUpdatedDomainEvent>
    {
        private readonly IRepository repository;

        public PaymentStatusUpdatedDomainEventHandler(IRepository repository)
        {
            this.repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        protected override void Handle(PaymentUpdatedDomainEvent domainEvent)
        {
            var order = repository.GetByIdAsync<Entities.Order>(domainEvent.OrderId).GetAwaiter().GetResult();

            order.PaymentId = domainEvent.AggregateRootId;
            order.UpdateStatus(Entities.OrderStatus.Paid);

            repository.UpdateAsync(order);
        }
    }
}