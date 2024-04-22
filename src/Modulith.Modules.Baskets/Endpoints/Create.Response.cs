namespace Modulith.Modules.Baskets.Endpoints;

public class CreateBasketResponse(Guid id)
{
    public Guid Id { get; set; } = id;
}