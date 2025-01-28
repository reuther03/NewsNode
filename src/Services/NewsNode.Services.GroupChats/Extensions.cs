using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.GroupChats.Hubs;

namespace NewsNode.Services.GroupChats;

public static class Extensions
{
    public static IServiceCollection AddGroupChat(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }

    public static IEndpointRouteBuilder MapHubGroupChats(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<GroupChatHub>("/groupchats");
        return endpoints;
    }
}