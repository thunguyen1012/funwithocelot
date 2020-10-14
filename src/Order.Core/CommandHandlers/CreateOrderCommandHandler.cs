using Common.Interfaces;
using Inventory.WebAPI.Client;
using MediatR;
using Order.Core.Commands;
using System;

namespace Order.Core.CommandHandlers
{
    public class CreateOrderCommandHandler : RequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IPublisher bus;
        private readonly IRepository repository;
        private readonly StockClient stockClient;

        public CreateOrderCommandHandler(IPublisher bus, IRepository repository, StockClient stockClient)
        {
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.repository = repository;
            this.stockClient = stockClient;
        }

        protected override Guid Handle(CreateOrderCommand command)
        {
            var order = Entities.Order.Create();

            order.Id = Guid.NewGuid();
            order.Start();
            order.UpdateProductId(command.ProductId);
            order.UpdateStatus(Entities.OrderStatus.New);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            stockClient.DecreaseProductQuantity(command.ProductId, 1);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            bus.Publish(order.GetEvents(), command.Header);

            repository.AddAsync(order);

            return order.Id;
        }
    }
}