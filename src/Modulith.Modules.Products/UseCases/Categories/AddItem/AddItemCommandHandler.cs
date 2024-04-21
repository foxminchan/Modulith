using System.Text.Json;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.AddItem;

public sealed class AddItemCommandHandler(
    IRepository<Category> repository,
    ILogger<AddItemCommandHandler> logger) : ICommandHandler<AddItemCommand, Result<CategoryId>>
{
    public async Task<Result<CategoryId>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        Category category = new(request.Name, request.Description);
        logger.LogInformation("[{Command}] Category information: {Category}",
            nameof(AddItemCommand), JsonSerializer.Serialize(category));
        await repository.AddAsync(category, cancellationToken);
        return Result<CategoryId>.Success(category.Id);
    }
}