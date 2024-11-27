using MediatR;
using NewsNode.Shared.Application.Kernel.Primitives.Result;

namespace NewsNode.Shared.Application.QueriesAndCommands.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;