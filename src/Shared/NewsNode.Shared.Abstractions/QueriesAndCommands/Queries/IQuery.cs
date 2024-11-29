using MediatR;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;

namespace NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;