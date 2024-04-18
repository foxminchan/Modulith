using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace Modulith.Infrastructure.ProblemDetails;

public sealed class DeveloperPageExceptionFilter : IDeveloperPageExceptionFilter
{
    private static readonly object _errorContextItemsKey = new();
    private static readonly MediaTypeHeaderValue _jsonMediaType = new(MediaTypeNames.Application.Json);

    private static readonly RequestDelegate _respondWithProblemDetails
        = RequestDelegateFactory.Create(
            (HttpContext context)
                => context.Items.TryGetValue(_errorContextItemsKey, out var errorContextItem) &&
                   errorContextItem is ErrorContext errorContext
                    ? new ErrorProblemDetailsResult(errorContext.Exception)
                    : null
        ).RequestDelegate;

    public async Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
    {
        var headers = errorContext.HttpContext.Request.GetTypedHeaders();
        var acceptHeader = headers.Accept;

        if (acceptHeader.Any(h => h.IsSubsetOf(_jsonMediaType)))
        {
            errorContext.HttpContext.Items.Add(_errorContextItemsKey, errorContext);
            await _respondWithProblemDetails(errorContext.HttpContext);
        }
        else
        {
            await next(errorContext);
        }
    }
}

public sealed class ErrorProblemDetailsResult(System.Exception ex) : IResult
{
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails = new()
        {
            Title = "An unhandled exception occurred while processing the request",
            Detail = $"{ex.GetType().Name}: {ex.Message}",
            Status = ex switch
            {
                BadHttpRequestException exception => exception.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            }
        };

        problemDetails.Extensions.Add("exception", ex.GetType().FullName);
        problemDetails.Extensions.Add("stack", ex.StackTrace);
        problemDetails.Extensions.Add("headers",
            httpContext.Request.Headers
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => (string)kvp.Value!)
        );
        problemDetails.Extensions.Add("routeValues", httpContext.GetRouteData().Values);
        problemDetails.Extensions.Add("query", httpContext.Request.Query);

        var endpoint = httpContext.GetEndpoint();

        if (endpoint is not null)
        {
            var routeEndpoint = endpoint as RouteEndpoint;
            var httpMethods = endpoint.Metadata.GetMetadata<IHttpMethodMetadata>()?.HttpMethods;
            problemDetails.Extensions.Add("endpoint",
                new
                {
                    endpoint.DisplayName,
                    routePattern = routeEndpoint?.RoutePattern.RawText,
                    routeOrder = routeEndpoint?.Order,
                    httpMethods = httpMethods is not null ? string.Join(", ", httpMethods) : ""
                });
        }

        await Results.Json(
            problemDetails,
            statusCode: problemDetails.Status,
            contentType: "application/problem+json"
        ).ExecuteAsync(httpContext);
    }
}