using Common.Messages.Order.Events;
using MediatR;
using Payment.Core.Commands;
using System;

namespace Payment.Core.EventHandlers
{
    public class PaymentRequestedDomainEventHandler
        : RequestHandler<OrderRequestedPaymentDomainEvent>
    {
        private readonly IMediator mediator;

        public PaymentRequestedDomainEventHandler(IMediator mediator)
        {
            this.mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }

        protected override void Handle(OrderRequestedPaymentDomainEvent domainEvent)
        {
            var paymentCommand = new PayCommand(domainEvent.AggregateRootId);
            mediator.Send(paymentCommand);
        }
    }
}