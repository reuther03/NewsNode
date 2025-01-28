using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Modules;

namespace NewsNode.Services.GroupChats;

public class GroupChatModule : IModule
{
    public const string BasePath = "groupchat-module";

    public string Name => "GroupChat";
    public string Path => BasePath;

    public void Register(IServiceCollection services)
    {
        services.AddGroupChat();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints => endpoints.MapHubGroupChats());
    }
}