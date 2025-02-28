using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.Post;

public class Comment : Entity<Guid>
{
    public string? Content { get; private set; }
    public DateTime PostedAt { get; private set; }
    public UserId CreatedBy { get; private set; }
    public int Likes { get; private set; }
    public int Bookmarks { get; private set; }
    public int Reposts { get; private set; }
    public ContentImg? ContentImg { get; private set; }

    private Comment()
    {
    }

    private Comment(Guid id, string content, UserId createdBy, ContentImg contentImg) : base(id)
    {
        Content = content;
        PostedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
        Likes = 0;
        Bookmarks = 0;
        Reposts = 0;
        ContentImg = contentImg;
    }

    public static Comment Create(string content, UserId createdBy, ContentImg contentImg)
        => new(Guid.NewGuid(), content, createdBy, contentImg);
}