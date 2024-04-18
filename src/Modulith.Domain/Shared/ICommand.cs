using MediatR;

namespace Modulith.Domain.Shared;

public interface ICommand<out TResponse> : IRequest<TResponse>, ITxRequest;