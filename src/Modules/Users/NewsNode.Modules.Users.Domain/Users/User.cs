using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Users.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public Name Username { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Location Location { get; private set; }

    private User()
    {
    }

    private User(UserId id, Name name, Email email, Password password, Location location) : base(id)
    {
        Username = name;
        Email = email;
        Password = password;
        Location = location;
    }

    public static User Create(Name name, Email email, Password password, Location location)
        => new(UserId.New(), name, email, password, location);
}