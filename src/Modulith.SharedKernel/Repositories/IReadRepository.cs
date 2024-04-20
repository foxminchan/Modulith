using Ardalis.Specification;
using Modulith.SharedKernel.Entities;

namespace Modulith.SharedKernel.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot;