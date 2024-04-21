using Modulith.Modules.Products.ViewModels;

namespace Modulith.Modules.Products.Endpoints.Categories;

public class ListCategoryResponse
{
    public List<CategoryVm> Categories { get; set; } = [];
}