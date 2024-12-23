using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Shared.Abstractions.Kernel.Database;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Remove(TEntity entity);
}