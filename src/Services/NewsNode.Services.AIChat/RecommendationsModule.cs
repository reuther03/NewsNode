// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Modules;

namespace NewsNode.Services.AIChat;

public class AIChatModule : IModule
{
    public const string BasePath = "AIChat-module";

    public string Name => "AIChat";
    public string Path => BasePath;

    public void Register(IServiceCollection services)
    {
        services.AddAIChat();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}