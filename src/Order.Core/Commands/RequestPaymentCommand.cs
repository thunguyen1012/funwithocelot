using Common;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace Order.Core.Commands
{
    [DataContract]
    public class RequestPaymentCommand : CommandBase, IRequest
    {
        [DataMember]
        public Guid OrderId { get; private set; }

        public RequestPaymentCommand()
        { }

        public RequestPaymentCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}