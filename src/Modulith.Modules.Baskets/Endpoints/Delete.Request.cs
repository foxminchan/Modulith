namespace Modulith.Modules.Baskets.Endpoints;

public sealed class DeleteBasketRequest(Guid id)
{
    public Guid Id { get; set; } = id;
}