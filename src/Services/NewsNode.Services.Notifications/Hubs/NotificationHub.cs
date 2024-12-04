using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace NewsNode.Services.Notifications.Hubs;

[Authorize]
public class NotificationHub : Hub
{

    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "Connected");

        await base.OnConnectedAsync();
    }
}