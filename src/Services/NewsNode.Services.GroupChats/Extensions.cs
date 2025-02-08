using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.GroupChats.Database;
using NewsNode.Services.GroupChats.Hubs;
using NewsNode.Shared.Abstractions.Services;
using NewsNode.Shared.Infrastructure.Postgres;
using NewsNode.Shared.Infrastructure.Services;

namespace NewsNode.Services.GroupChats;

public static class Extensions
{
    public static IServiceCollection AddGroupChat(this IServiceCollection services)
    {
        // services.AddPostgres<GroupChatDbContext>();
        // services.AddScoped<GroupChatDbContext>();
        services.AddSingleton<IHubConnectionService, HubConnectionService>();
        services.AddSignalR();
        return services;
    }

    public static IEndpointRouteBuilder MapHubGroupChats(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<GroupChatHub>("/groupchats");
        return endpoints;
    }
}