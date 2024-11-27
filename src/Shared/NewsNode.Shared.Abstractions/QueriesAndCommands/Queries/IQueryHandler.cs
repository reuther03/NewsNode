using MediatR;
using NewsNode.Shared.Application.Kernel.Primitives.Result;

namespace NewsNode.Shared.Application.QueriesAndCommands.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;