using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Users.Application.Abstractions.Database;
using NewsNode.Modules.Users.Domain.Users;

namespace NewsNode.Modules.Users.Infrastructure.Database;

internal class UsersDbContext : DbContext, IUsersDbContext
{
    public DbSet<User> Users => Set<User>();

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}