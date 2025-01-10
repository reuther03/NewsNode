using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class PostViews : Entity<Guid>
{
    public PostId PostId { get; private set; }
    public int Views { get; private set; }
}