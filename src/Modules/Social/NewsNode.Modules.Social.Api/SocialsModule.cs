using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Modules.Social.Application;
using NewsNode.Modules.Social.Domain;
using NewsNode.Modules.Social.Infrastructure;
using NewsNode.Shared.Abstractions.Modules;

// ReSharper disable ClassNeverInstantiated.Global

namespace NewsNode.Modules.Social.Api;

public class SocialsModule : IModule
{
    public const string BasePath = "socials-module";

    public string Name => "Socials";
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