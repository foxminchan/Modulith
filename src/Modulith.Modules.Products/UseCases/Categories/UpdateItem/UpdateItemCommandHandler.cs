using System.Text.Json;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.CategoryAggregate.Specifications;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.UpdateItem;

public sealed class UpdateItemCommandHandler(IRepository<Category> repository, ILogger<UpdateItemCommandHandler> logger)
    : ICommandHandler<UpdateItemCommand, Result<CategoryVm>>
{
    public async Task<Result<CategoryVm>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        CategoryByIdSpec spec = new(request.Id);
        var category = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        category.Update(request.Name, request.Description);
        logger.LogInformation("[{Command}] Category information: {Category}",
            nameof(UpdateItemCommand), JsonSerializer.Serialize(category));
        await repository.UpdateAsync(category, cancellationToken);
        var response = CategoryVm.FromEntity(category);
        return Result<CategoryVm>.Success(response);
    }
}