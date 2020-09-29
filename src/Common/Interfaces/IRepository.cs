using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : BaseAggregateRoot;

        Task<List<T>> ListAsync<T>() where T : BaseAggregateRoot;

        Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseAggregateRoot;

        Task<T> AddAsync<T>(T entity) where T : BaseAggregateRoot;

        Task UpdateAsync<T>(T entity) where T : BaseAggregateRoot;

        Task DeleteAsync<T>(T entity) where T : BaseAggregateRoot;
    }
}