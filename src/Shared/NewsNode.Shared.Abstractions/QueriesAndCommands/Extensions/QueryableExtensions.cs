using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NewsNode.Shared.Abstractions.Kernel.Pagination;

namespace NewsNode.Shared.Abstractions.QueriesAndCommands.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    public static async Task<PaginatedList<TOut>> ToPagedListAsync<T, TOut>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        Expression<Func<T, TOut>> mappingExpression,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);

        var results = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(mappingExpression)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TOut>(page, pageSize, count, results);
    }
}