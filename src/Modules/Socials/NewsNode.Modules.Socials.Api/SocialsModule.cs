// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Modules.Socials.Application;
using NewsNode.Modules.Socials.Domain;
using NewsNode.Modules.Socials.Infrastructure;
using NewsNode.Shared.Abstractions.Modules;

namespace NewsNode.Modules.Socials.Api;

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