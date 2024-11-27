using MediatR;
using NewsNode.Shared.Application.Kernel.Primitives.Result;

namespace NewsNode.Shared.Application.QueriesAndCommands.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;