using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Notifications;

[Authorize]
public class NotificationHub : Hub
{
    private const string ReceiveMessage = "ReceiveMessage";

    private readonly IUserService _userService;

    public NotificationHub(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).SendAsync(ReceiveMessage, "Connected");

        await base.OnConnectedAsync();
    }

    public async Task FollowedNotification(Guid followerId)
    {
        await Clients.User(_userService.UserId!).SendAsync(ReceiveMessage, followerId);
    }
}