using System.Data;
using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modulith.SharedKernel.Events;
using Modulith.SharedKernel.Shared;

namespace Modulith.Persistence;

[DebuggerStepThrough]
public sealed class TxBehavior<TRequest, TResponse>(
    IPublisher publisher,
    IDatabaseFacade databaseFacade,
    IDomainEventContext eventContext,
    ILogger<TxBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ITxRequest) return await next();

        const string behavior = nameof(TxBehavior<TRequest, TResponse>);

        logger.LogInformation("[{Behavior}] {Request} handled command {CommandName}", behavior, request,
            request.GetType().FullName);
        logger.LogDebug("[{Behavior}] {Request} handled command {CommandName} with {CommandData}", behavior, request,
            request.GetType().FullName, request);
        logger.LogInformation("[{Behavior}]  {Request} begin transaction for command {CommandName}", behavior, request,
            request.GetType().FullName);

        var strategy = databaseFacade.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await databaseFacade.Database
                .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            var response = await next();

            logger.LogInformation("[{Behavior}] {Request} transaction {TransactionId} begin for command {CommandName}",
                behavior, request,
                transaction.TransactionId, request.GetType().FullName);

            var domainEvents = eventContext.GetDomainEvents().ToList();

            logger.LogInformation(
                "[{Behavior}] {Request} transaction {TransactionId} begin for command {CommandName} with {DomainEventsCount} domain events",
                behavior,
                request, transaction.TransactionId, request.GetType().FullName, domainEvents.Count);

            var tasks = domainEvents.Select(async
                domainEvent =>
            {
                await publisher.Publish(
                    new EventWrapper(domainEvent), cancellationToken);

                logger.LogDebug(
                    "[{Behavior}] Published domain event {DomainEventName} with payload {DomainEventContent}",
                    behavior, domainEvent.GetType().FullName,
                    JsonSerializer.Serialize(domainEvent));
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);

            await transaction.CommitAsync(cancellationToken);

            return response;
        }).ConfigureAwait(false);
    }
}