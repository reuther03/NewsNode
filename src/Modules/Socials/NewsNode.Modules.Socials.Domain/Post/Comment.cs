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

    private Comment()
    {
    }

    // private readonly List<PostFileUrl> _fileUrls = [];
    // public IReadOnlyList<PostFileUrl> FileUrls => _fileUrls.AsReadOnly();
}