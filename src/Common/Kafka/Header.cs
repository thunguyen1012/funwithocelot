using Common.Interfaces;
using System;

namespace Common.Kafka
{
    public class Header : IHeader
    {
        public Guid CorrelationId { get; private set; }
        public string UserName { get; private set; }

        public Header(Guid correlationId, string userName)
        {
            this.CorrelationId = correlationId;
            this.UserName = userName;
        }
    }
}