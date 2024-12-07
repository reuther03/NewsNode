using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.Article;

public class Post : AggregateRoot<ArticleId>
{
    public string Content { get; private set; }
    public DateTime PostedAt { get; private set; }
    public UserId CreatedBy { get; private set; }
    public int Likes { get; private set; }
    public int Bookmarks { get; private set; }
    public int Reposts { get; private set; }
    // moze dislikes

    private readonly List<Comment> _comments = [];
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();

    // private readonly List<ArticleFileUrl> _fileUrls = [];
    // public IReadOnlyList<ArticleFileUrl> FileUrls => _fileUrls.AsReadOnly();

    public Post()
    {
    }

    public Post(ArticleId id, string content, UserId createdBy) : base(id)
    {
        Content = content;
        PostedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
        Likes = 0;
        Bookmarks = 0;
        Reposts = 0;
    }

    public void AddComment(Comment comment)
        => _comments.Add(comment);
}