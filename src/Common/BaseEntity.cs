using Common.Interfaces;
using System;

namespace Common
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get;  set; }
    }
}