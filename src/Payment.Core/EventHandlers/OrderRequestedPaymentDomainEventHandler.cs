using MediatR;
using Payment.Core.Commands;
using Payment.Core.Events;
using System;

namespace Payment.Core.EventHandlers
{
    public class OrderRequestedPaymentDomainEventHandler
        //: RequestHandler<OrderRequestedPaymentDomainEvent, Guid>
        : RequestHandler<OrderRequestedPaymentDomainEvent>
    {
        private readonly IMediator mediator;

        public OrderRequestedPaymentDomainEventHandler(IMediator mediator)
        {
            this.mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }

        protected override void Handle(OrderRequestedPaymentDomainEvent domainEvent)
        {
            // Call directly???
            var paymentCommand = new PayCommand(domainEvent.AggregateRootId);
            var id = mediator.Send(paymentCommand).GetAwaiter().GetResult();
            //return id;
        }
    }
}