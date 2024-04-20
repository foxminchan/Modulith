using MediatR;

namespace Modulith.SharedKernel.Shared;

public interface IQuery<out TResponse> : IRequest<TResponse>;