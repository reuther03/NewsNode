using NewsNode.Shared.Application.Kernel.Primitives;
using NewsNode.Shared.Application.Kernel.ValueObjects;

namespace NewsNode.Modules.Users.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
}