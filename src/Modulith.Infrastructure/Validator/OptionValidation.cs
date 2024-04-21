using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Modulith.Infrastructure.Validator;

public sealed class OptionValidation<TOption>(IServiceProvider serviceProvider)
    : IValidateOptions<TOption> where TOption : class
{
    public ValidateOptionsResult Validate(string? name, TOption options)
    {
        if (name is not null && name != Options.DefaultName) return ValidateOptionsResult.Skip;

        Guard.Against.Null(options);

        using var scope = serviceProvider.CreateScope();
        var result = scope.ServiceProvider.GetRequiredService<IValidator<TOption>>().Validate(options);

        if (result.IsValid) return ValidateOptionsResult.Success;

        var type = options.GetType().Name;
        var errors = result.Errors
            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
            .ToList();

        return ValidateOptionsResult.Fail($"{type} has validation errors: {string.Join(", ", errors)}");
    }
}