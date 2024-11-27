using MediatR;
using NewsNode.Shared.Application.Kernel.Primitives.Result;

namespace NewsNode.Shared.Application.QueriesAndCommands.Commands;

/// <summary>
/// Marker interface for <see cref="ICommand"/> and <see cref="ICommand{TResponse}"/>
/// </summary>
public interface ICommandBase;

/// <summary>
/// Marker interface for commands
/// </summary>
public interface ICommand : IRequest<Result>, ICommandBase;

/// <summary>
/// Marker interface for commands with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase;