using Modulith.Modules.Baskets.ViewModels;

namespace Modulith.Modules.Baskets.Endpoints;

public sealed class UpdateBasketResponse
{
    public CustomerBasketVm? Basket { get; set; }
}