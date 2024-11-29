using MediatR;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;

namespace NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;