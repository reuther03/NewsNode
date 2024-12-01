using System.Diagnostics.CodeAnalysis;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Shared.Abstractions.Services;

public interface IUserService
{
    [MemberNotNullWhen(true, nameof(UserId), nameof(Email), nameof(UserName))]
    public bool IsAuthenticated { get; }

    public UserId? UserId { get; }
    public Email? Email { get; }
    public Name? UserName { get; }
}