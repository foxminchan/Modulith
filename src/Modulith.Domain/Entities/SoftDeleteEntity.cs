namespace Modulith.Domain.Entities;

public sealed class SoftDeleteEntity : EntityBase
{
    public bool IsDeleted { get; set; }
}