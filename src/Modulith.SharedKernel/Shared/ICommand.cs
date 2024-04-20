using MediatR;

namespace Modulith.SharedKernel.Shared;

public interface ICommand<out TResponse> : IRequest<TResponse>, ITxRequest;