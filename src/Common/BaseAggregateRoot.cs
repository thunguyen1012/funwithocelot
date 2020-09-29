using System;
using System.Collections.Generic;

namespace Common
{
    public abstract class BaseAggregateRoot : BaseEntity
    {
        private readonly Dictionary<Type, Action<object>> handlers = new Dictionary<Type, Action<object>>();
        private readonly List<BaseDomainEvent> domainEvents = new List<BaseDomainEvent>();

        public int Version { get; private set; }

        public BaseAggregateRoot()
        {
            Version = 0;
        }

        protected void Register<T>(Action<T> when)
        {
            handlers.Add(typeof(T), e => when((T)e));
        }

        protected void Raise(BaseDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
            handlers[domainEvent.GetType()](domainEvent);
            Version++;
        }

        public IReadOnlyCollection<BaseDomainEvent> GetEvents()
        {
            return domainEvents;
        }

        public void Apply(BaseDomainEvent domainEvent)
        {
            handlers[domainEvent.GetType()](domainEvent);
            Version++;
        }
    }
}