using Ardalis.GuardClauses;
using Ardalis.Result;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Modulith.Infrastructure.Exception;

public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "[{Handler}] Exception occurred: {ExceptionMessage}", nameof(ExceptionHandler),
            exception.Message);

        switch (exception)
        {
            case ValidationException { Errors: not null } validationException:
                await HandleValidationException(httpContext, validationException, cancellationToken);
                break;

            case NotFoundException notFoundException:
                await HandleNotFoundException(httpContext, notFoundException, cancellationToken);
                break;

            default:
                await HandleDefaultException(httpContext, exception, cancellationToken);
                break;
        }

        return true;
    }

    private static async Task HandleValidationException(
        HttpContext httpContext,
        ValidationException validationException,
        CancellationToken cancellationToken)
    {
        var validationErrorModel = Result.Invalid(validationException
            .Errors
            .Select(e => new ValidationError(
                e.PropertyName,
                e.ErrorMessage,
                StatusCodes.Status400BadRequest.ToString(),
                ValidationSeverity.Info
            )).ToList());
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(validationErrorModel.ValidationErrors, cancellationToken);
    }

    private static async Task HandleNotFoundException(
        HttpContext httpContext,
        System.Exception notFoundException,
        CancellationToken cancellationToken)
    {
        var notFoundErrorModel = Result.NotFound(notFoundException.Message);
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(notFoundErrorModel.Errors, cancellationToken);
    }

    private static async Task HandleDefaultException(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        Microsoft.AspNetCore.Mvc.ProblemDetails details = new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = exception.GetType().Name,
            Title = "An error occurred while processing your request",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method}{httpContext.Request.Path}"
        };
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
    }
}