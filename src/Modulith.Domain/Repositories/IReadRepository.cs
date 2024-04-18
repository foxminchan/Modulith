using Ardalis.Specification;
using Modulith.Domain.Entities;

namespace Modulith.Domain.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot;