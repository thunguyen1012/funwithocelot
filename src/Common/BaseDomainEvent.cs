using Common.Interfaces;
using System;

namespace Common
{
    public class BaseDomainEvent : IDomainEvent
    {
        public Guid AggregateRootId { get; private set; }
        public int Version { get; private set; }
        public DateTime DateOccurred { get; private set; }
        public IHeader Header { get; private set; }

        protected BaseDomainEvent(Guid aggregateRootId, int version, DateTime createdDate, IHeader header)
        {
            this.AggregateRootId = aggregateRootId;
            this.Version = version;
            this.DateOccurred = createdDate;
            this.Header = header;
        }

        public static BaseDomainEvent Create(Guid aggregateRootId, int version, DateTime createdDate)
        {
            var domainEvent = new BaseDomainEvent(aggregateRootId, version, createdDate, null);
            return domainEvent;
        }

        public void SetHeader(IHeader header) => this.Header = header;
    }
}