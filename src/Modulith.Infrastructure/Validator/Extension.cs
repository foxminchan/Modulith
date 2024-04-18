using System.Diagnostics;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Modulith.Infrastructure.Validator;

public static class Extension
{
    public static OptionsBuilder<TOption> ValidateFluentValidation<TOption>(this OptionsBuilder<TOption> builder)
        where TOption : class
    {
        builder.Services.AddSingleton<IValidateOptions<TOption>>(service => new OptionValidation<TOption>(service));
        return builder;
    }

    public static async Task HandleValidationAsync<TRequest>(this IValidator<TRequest> validator, TRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        var failures = validationResult.Errors;
        if (failures.Count != 0) throw new ValidationException(failures);
    }

    [DebuggerStepThrough]
    public static IServiceCollection AddValidator(this IServiceCollection services)
        => services.Scan(scan => scan
            .FromAssemblies(AssemblyReference.Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
}