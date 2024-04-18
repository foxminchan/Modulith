using MediatR;

namespace Modulith.Domain.Shared;

public interface IQuery<out TResponse> : IRequest<TResponse>;