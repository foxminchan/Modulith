namespace Modulith.Infrastructure.Endpoint;

public interface IEndpointWithoutRequest<TResponse> : IEndpointBase
{
    Task<TResponse> HandleAsync(CancellationToken cancellationToken = default);
}