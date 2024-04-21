using Ardalis.Result;
using Modulith.Modules.Products.ViewModels;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class ListProductResponse
{
    public PagedInfo? PagedInfo { get; set; }
    public List<ProductVm> Products { get; set; } = [];
}