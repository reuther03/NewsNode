using Microsoft.Extensions.DependencyInjection;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Infrastructure.Database;
using NewsNode.Modules.Socials.Infrastructure.Database.Repositories;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres<SocialsDbContext>()
            .AddScoped<ISocialsDbContext, SocialsDbContext>()
            .AddScoped<IUserProfileRepository, UserProfileRepository>()
            .AddUnitOfWork<IUnitOfWork, UnitOfWork>();

        return services;
    }
}