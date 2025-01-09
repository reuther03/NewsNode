using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class LocalRecommendation : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public Location Location { get; private set; }

}