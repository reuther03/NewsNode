using Microsoft.AspNetCore.SignalR;
using NewsNode.Shared.Abstractions.Services;
using NewsNode.Shared.Infrastructure.Services;

namespace NewsNode.Services.GroupChats.Hubs;

public class GroupChatHub : Hub
{
    private readonly IHubConnectionService _hubConnectionService;

    public GroupChatHub(IHubConnectionService hubConnectionService)
    {
        _hubConnectionService = hubConnectionService;
    }
}