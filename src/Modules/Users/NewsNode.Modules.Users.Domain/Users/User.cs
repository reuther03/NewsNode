using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Application.Kernel.Primitives;

namespace NewsNode.Modules.Users.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public Name Username { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    private User()
    {
    }

    private User(UserId id, Name name, Email email, Password password) : base(id)
    {
        Username = name;
        Email = email;
        Password = password;
    }

    public static User Create(Name name, Email email, Password password)
        => new(UserId.New(), name, email, password);
}