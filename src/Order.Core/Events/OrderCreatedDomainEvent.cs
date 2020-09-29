﻿using Common;
using Common.Interfaces;
using MediatR;
using System;

namespace Order.Core.Events
{
    public class OrderCreatedDomainEvent : BaseDomainEvent, IRequest<Guid>
    {
        public OrderCreatedDomainEvent(Guid aggregateRootId, int version,
            DateTime createdDate, IHeader header)
            : base(aggregateRootId, version, createdDate, header)
        {
        }

        public static OrderCreatedDomainEvent Create(BaseAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");

            var domainEvent = new OrderCreatedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null);

            return domainEvent;
        }
    }
}