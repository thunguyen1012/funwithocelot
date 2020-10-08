using Common.Interfaces;
using MediatR;
using Order.Core.Commands;
using System;

namespace Order.Core.CommandHandlers
{
    public class CreateOrderCommandHandler : RequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IPublisher bus;
        private readonly IRepository repository;

        public CreateOrderCommandHandler(IPublisher bus, IRepository repository)
        {
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.repository = repository;
        }

        protected override Guid Handle(CreateOrderCommand command)
        {
            var order = Entities.Order.Create();

            order.Id = Guid.NewGuid();
            order.Start();
            order.UpdateProductId(command.ProductId);
            order.UpdateStatus(Entities.OrderStatus.New);

            // TODO VERIFY
            bus.Publish(order.GetEvents(), command.Header);

            repository.AddAsync(order);

            return order.Id;
        }
    }
}