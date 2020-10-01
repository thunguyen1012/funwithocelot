using System;
using System.Collections.Generic;

namespace Common
{
    public abstract class BaseAggregateRoot : BaseEntity
    {
        private readonly Dictionary<string, Action<object>> handlers = new Dictionary<string, Action<object>>();
        private readonly List<BaseDomainEvent> domainEvents = new List<BaseDomainEvent>();

        public int Version { get; private set; }

        public BaseAggregateRoot()
        {
            Version = 0;
        }

        protected void Register<T>(Action<T> when)
        {
            handlers.Add(typeof(T).Name, e => when((T)e));
        }

        protected void Raise(BaseDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
            handlers[domainEvent.GetType().Name](domainEvent);
            Version++;
        }

        public IReadOnlyCollection<BaseDomainEvent> GetEvents()
        {
            return domainEvents;
        }

        public void Apply(BaseDomainEvent domainEvent)
        {
            handlers[domainEvent.GetType().Name](domainEvent);
            Version++;
        }
    }
}