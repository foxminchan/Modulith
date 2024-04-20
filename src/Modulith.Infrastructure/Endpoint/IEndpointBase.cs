using Microsoft.AspNetCore.Routing;

namespace Modulith.Infrastructure.Endpoint;

public interface IEndpointBase
{
    void MapEndpoint(IEndpointRouteBuilder app);
}