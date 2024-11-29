using Microsoft.Extensions.DependencyInjection;
using NewsNode.Modules.Users.Application.Abstractions;
using NewsNode.Modules.Users.Application.Abstractions.Database;
using NewsNode.Modules.Users.Infrastructure.Database;
using NewsNode.Modules.Users.Infrastructure.Database.Repositories;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres<UsersDbContext>()
            .AddScoped<IUsersDbContext, UsersDbContext>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddUnitOfWork<IUnitOfWork, UnitOfWork>();

        return services;
    }
}