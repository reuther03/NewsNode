using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Modules.Socials.Domain.Article;

public class PostFileUrl : Entity<Guid>
{
    public string FileUrl { get; private set; }
    public string FileName { get; private set; }

    private PostFileUrl()
    {
    }

    public PostFileUrl(Guid id, string fileUrl, string fileName) : base(id)
    {
        FileUrl = fileUrl;
        FileName = fileName;
    }

    public static PostFileUrl Create(string fileUrl, string fileName)
        => new(Guid.NewGuid(), fileUrl, fileName);
}