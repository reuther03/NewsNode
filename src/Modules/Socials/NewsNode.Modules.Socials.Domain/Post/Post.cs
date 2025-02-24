using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.Post;

public class Post : AggregateRoot<PostId>
{
    private readonly List<Hashtag> _hashtags = [];

    private readonly List<Comment> _comments = [];
    private readonly List<PostImg> _postimgs = [];

    public string Content { get; private set; }
    public DateTime PostedAt { get; private set; }
    public UserId CreatedBy { get; private set; }
    public int Likes { get; private set; }
    public int Bookmarks { get; private set; }
    public int Reposts { get; private set; }
    public IReadOnlyList<Hashtag> Hashtags => _hashtags.AsReadOnly();
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();

    public IReadOnlyList<PostImg> PostImgs => _postimgs.AsReadOnly();

    public Post()
    {
    }

    public Post(PostId id, string content, List<Hashtag> hashtags, UserId createdBy) : base(id)
    {
        Content = content;
        PostedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
        Likes = 0;
        Bookmarks = 0;
        Reposts = 0;
        _hashtags.AddRange(hashtags);
    }

    public static Post Create(string content, List<Hashtag> hashtags, UserId createdBy)
        => new(PostId.New(), content, hashtags, createdBy);

    public void AddComment(Comment comment)
        => _comments.Add(comment);

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

    public void AddPostImg(PostImg postImg)
        => _postimgs.Add(postImg);
}