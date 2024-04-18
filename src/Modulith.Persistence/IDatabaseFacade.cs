using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Modulith.Persistence;

public interface IDatabaseFacade
{
    public DatabaseFacade Database { get; }
}