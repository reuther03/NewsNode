using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Users.Domain.Users;

namespace NewsNode.Modules.Users.Application.Abstractions.Database;

public interface IUsersDbContext
{
    DbSet<User> Users { get; }
}