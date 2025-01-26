using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Dtos;

public sealed class PostFilter
{
    public string? SearchValue { get; init; }
    public List<string> Hashtags { get; init; } = [];
    public Guid? PostedBy { get; init; }
    public bool IsTrending { get; init; }

    public IQueryable<Post> Filter(IQueryable<Post> query)
    {
        if (!string.IsNullOrWhiteSpace(SearchValue))
        {
            query = query.Where(x => x.Content.Contains(SearchValue));
        }

        if (Hashtags.Count != 0)
        {
            query = query.Where(x => x.Hashtags.Any(y => Hashtags.Contains(y.Value)));
        }

        if (PostedBy.HasValue)
        {
            query = query.Where(x => x.CreatedBy == UserId.From(PostedBy.Value));
        }

        if (IsTrending)
        {
            query = query.Where(x => x.Likes > 100);
        }

        return query;
    }
}