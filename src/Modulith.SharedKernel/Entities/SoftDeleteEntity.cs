namespace Modulith.SharedKernel.Entities;

public class SoftDeleteEntity : EntityBase
{
    public bool IsDeleted { get; set; }
}