using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;

namespace NewsNode.Shared.Abstractions.Kernel.Database;

public interface IBaseUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}