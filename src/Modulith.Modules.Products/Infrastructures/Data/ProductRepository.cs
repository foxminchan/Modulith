using Ardalis.Specification.EntityFrameworkCore;
using Modulith.SharedKernel.Entities;
using Modulith.SharedKernel.Repositories;

namespace Modulith.Modules.Products.Infrastructures.Data;

public sealed class ProductRepository<T>(ProductDbContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot;