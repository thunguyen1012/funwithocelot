using Common.Interfaces;
using MediatR;
using Payment.Core.Commands;
using System;

namespace Payment.Core.CommandHandlers
{
    public class PayCommandHandler : RequestHandler<PayCommand, Guid>
    {
        private readonly IPublisher bus;

        private readonly IRepository repository;

        public PayCommandHandler(IPublisher bus, IRepository repository)
        {
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.repository = repository;
        }

        protected override Guid Handle(PayCommand command)
        {
            var payment = Entities.Payment.Create();
            payment.Id = Guid.NewGuid();

            payment.Start();
            payment.UpdateOrderId(command.OrderId);
            payment.UpdateStatus(Entities.PaymentStatus.New);

            bus.Publish(payment.GetEvents(), command.Header);

            repository.AddAsync(payment);


            return payment.Id;
        }
    }
}