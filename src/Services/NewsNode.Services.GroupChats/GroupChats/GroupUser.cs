using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.GroupChats.GroupChats;

public class GroupUser : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public Name UserName { get; private set; }
    public GroupUserRole Role { get; private set; }
    public Guid GroupChatId { get; private set; }

    private GroupUser()
    {
    }

    private GroupUser(Guid id, UserId userId, Name userName, GroupUserRole role, Guid groupChatId) : base(id)
    {
        UserId = userId;
        UserName = userName;
        Role = role;
        GroupChatId = groupChatId;
    }

    public static GroupUser Create(UserId userId, Name userName, GroupUserRole role, Guid groupChatId) =>
        new(Guid.NewGuid(), userId, userName, role, groupChatId);
}