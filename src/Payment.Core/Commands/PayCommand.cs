using Common;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Payment.Core.Commands
{
    [DataContract]
    public class PayCommand : CommandBase, IRequest<Guid>
    {
        [DataMember]
        public Guid OrderId { get; private set; }

        public PayCommand()
        { }

        public PayCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}