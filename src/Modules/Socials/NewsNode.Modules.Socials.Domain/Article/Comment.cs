using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.Article;

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

    // private readonly List<ArticleFileUrl> _fileUrls = [];
    // public IReadOnlyList<ArticleFileUrl> FileUrls => _fileUrls.AsReadOnly();
}