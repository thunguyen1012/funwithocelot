using Common.Interfaces;
using MediatR;
using Order.Core.Commands;
using System;

namespace Order.Core.CommandHandlers
{
    public class RequestPaymentCommandHandler : RequestHandler<RequestPaymentCommand, Guid>
    {
        private readonly IPublisher bus;
        private readonly IRepository repository;

        public RequestPaymentCommandHandler(IPublisher bus, IRepository repository)
        {
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override Guid Handle(RequestPaymentCommand command)
        {
            var order = repository.GetByIdAsync<Entities.Order>(command.OrderId).GetAwaiter().GetResult();
            order.RequestPayment();

            // TODO VERIFY
            bus.Publish(order.GetEvents(), command.Header).GetAwaiter().GetResult();

            return order.Id;
        }
    }
}