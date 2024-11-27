using NewsNode.Shared.Application.Kernel.Primitives.Result;

namespace NewsNode.Shared.Application.Kernel.Database;

public interface IBaseUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}