using Ardalis.Specification;
using Modulith.SharedKernel.Entities;

namespace Modulith.SharedKernel.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot;