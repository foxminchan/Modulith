using Ardalis.Specification;
using Modulith.Domain.Entities;

namespace Modulith.Domain.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot;