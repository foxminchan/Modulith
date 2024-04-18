using System.Diagnostics;
using System.Reflection;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Modulith.Infrastructure.Logging;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        const string behavior = nameof(LoggingBehavior<TRequest, TResponse>);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("[{Behavior}] Handle request={Request} and response={Response}",
                behavior, typeof(TRequest).FullName, typeof(TResponse).FullName);

            var myType = request.GetType();
            var props = new List<PropertyInfo>(myType.GetProperties());
            foreach (var prop in props)
            {
                logger.LogInformation("[{Behavior}] Property {Property}={Value}", behavior,
                    prop.Name, prop.GetValue(request));
            }
        }

        var sw = Stopwatch.StartNew();
        var response = await next();

        logger.LogInformation(
            "[{Behavior}] The request handled {RequestName} with {Response} in {ElapsedMilliseconds} ms",
            behavior,
            typeof(TRequest).FullName, response,
            sw.ElapsedMilliseconds);

        sw.Stop();

        var timeTaken = sw.Elapsed;

        if (timeTaken.Seconds > 3)
            logger.LogWarning("[{Behavior}] The request {Request} took {TimeTaken} seconds.",
                behavior, typeof(TRequest).FullName, timeTaken.Seconds);

        return response;
    }
}