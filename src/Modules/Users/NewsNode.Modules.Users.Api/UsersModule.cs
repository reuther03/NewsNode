using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Modules.Users.Application;
using NewsNode.Modules.Users.Domain;
using NewsNode.Modules.Users.Infrastructure;
using NewsNode.Shared.Abstractions.Modules;

// ReSharper disable ClassNeverInstantiated.Global

namespace NewsNode.Modules.Users.Api;

public class UsersModule : IModule
{
    public const string BasePath = "users-module";

    public string Name => "Users";
    public string Path => BasePath;

    public void Register(IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}