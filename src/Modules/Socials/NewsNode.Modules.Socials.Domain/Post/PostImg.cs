using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Modules.Socials.Domain.Post;

public class PostImg : Entity<Guid>
{
    public string FileUrl { get; private set; }
    public string FileName { get; private set; }

    private PostImg()
    {
    }

    public PostImg(Guid id, string fileUrl, string fileName) : base(id)
    {
        FileUrl = fileUrl;
        FileName = fileName;
    }

    public static PostImg Create(string fileUrl, string fileName)
        => new(Guid.NewGuid(), fileUrl, fileName);
}