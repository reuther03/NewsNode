using Microsoft.Extensions.DependencyInjection;
using NewsNode.Modules.Users.Infrastructure.Database;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres<UsersDbContext>();
            // .AddScoped<IUserDbContext, UsersDbContext>()

        return services;
    }
}