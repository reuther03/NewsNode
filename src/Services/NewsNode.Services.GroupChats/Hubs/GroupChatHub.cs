using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.GroupChats.Hubs;

[Authorize]
public class GroupChatHub : Hub
{
    private readonly IHubConnectionService _hubConnectionService;

    public GroupChatHub(IHubConnectionService hubConnectionService)
    {
        _hubConnectionService = hubConnectionService;
    }

    public override async Task OnConnectedAsync()
    {
        _hubConnectionService.Connect(Context.ConnectionId, Context.User!.Identity!.Name!);

        // await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "Connected to chat");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _hubConnectionService.Disconnect(Context.ConnectionId, Context.User!.Identity!.Name!);

        await base.OnDisconnectedAsync(exception);
    }
}