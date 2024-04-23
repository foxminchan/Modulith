using Ardalis.Specification.EntityFrameworkCore;
using Modulith.SharedKernel.Entities;
using Modulith.SharedKernel.Repositories;

namespace Modulith.Modules.Orders.Infrastructures.Data;

public sealed class OrderRepository<T>(OrderDbContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot;