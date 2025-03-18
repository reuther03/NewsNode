using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.Post;

public class Post : AggregateRoot<PostId>
{
    private readonly List<Hashtag> _hashtags = [];

    public string? Content { get; private set; }
    public DateTime PostedAt { get; private set; }
    public UserId CreatedBy { get; private set; }
    public int Likes { get; private set; }
    public int Bookmarks { get; private set; }
    public int Reposts { get; private set; }
    public ContentImg? ContentImg { get; private set; }
    public IReadOnlyList<Hashtag> Hashtags => _hashtags.AsReadOnly();


    public Post()
    {
    }

    private Post(PostId id, string? content, List<Hashtag> hashtags, UserId createdBy, ContentImg? contentImg) : base(id)
    {
        Content = content ?? null;
        PostedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
        Likes = 0;
        Bookmarks = 0;
        Reposts = 0;
        _hashtags.AddRange(hashtags);
        ContentImg = contentImg ?? null;
    }

    public static Post Create(string? content, List<Hashtag> hashtags, UserId createdBy, ContentImg? contentImg)
        => new(PostId.New(), content, hashtags, createdBy, contentImg);

    public void PerformAction(PostActionType actionType)
    {
        switch (actionType)
        {
            case PostActionType.Liked:
                Likes++;
                break;
            case PostActionType.Disliked:
                Likes--;
                break;
            case PostActionType.Bookmarked:
                Bookmarks++;
                break;
            case PostActionType.Reposted:
                Reposts++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
        }
    }
}