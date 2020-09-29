using Common;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Order.Core.Commands
{
    [DataContract]
    public class CreateOrderCommand : CommandBase, IRequest<Guid>
    {
        [DataMember]
        public Guid ProductId { get; private set; }

        public CreateOrderCommand()
        { }

        public CreateOrderCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}