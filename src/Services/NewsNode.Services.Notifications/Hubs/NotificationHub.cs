using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Notifications.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    private readonly IHubConnectionService _connections;

    public NotificationHub(IHubConnectionService connections)
    {
        _connections = connections;
    }

    public override async Task OnConnectedAsync()
    {
        _connections.Connect(Context.ConnectionId, Context.User!.Identity!.Name!);

        await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "Connected");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _connections.Disconnect(Context.ConnectionId, Context.User!.Identity!.Name!);

        await base.OnDisconnectedAsync(exception);
    }
}