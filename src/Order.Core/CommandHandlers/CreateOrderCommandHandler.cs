using Common.Interfaces;
using MediatR;
using Order.Core.Commands;
using System;

namespace Order.Core.CommandHandlers
{
    public class CreateOrderCommandHandler : RequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IPublisher bus;

        public CreateOrderCommandHandler(IPublisher bus)
        {
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        protected override Guid Handle(CreateOrderCommand command)
        {
            var order = Entities.Order.Create();

            order.Start();
            order.UpdateProductId(command.ProductId);
            order.UpdateStatus(Entities.OrderStatus.New);

            // TODO VERIFY
            bus.Publish(order.GetEvents(), command.Header).GetAwaiter().GetResult();

            return order.Id;
        }
    }
}