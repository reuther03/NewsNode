using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NewsNode.Services.GroupChats.Database;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.GroupChats.Hubs;

[Authorize]
public class GroupChatHub : Hub
{
    private readonly IHubConnectionService _hubConnectionService;
    private readonly IUserService _userService;
    private readonly GroupChatsDbContext _context;

    public GroupChatHub(IHubConnectionService hubConnectionService, IUserService userService, GroupChatsDbContext context)
    {
        _hubConnectionService = hubConnectionService;
        _userService = userService;
        _context = context;
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

    public async Task ConnectToGroupChat(Guid groupChatId)
    {
        var userId = _userService.UserId;
        if (userId is null)
            throw new UnauthorizedAccessException();

        var groupChat = await _context.GroupChats.FindAsync(groupChatId);
        if (groupChat is null)
            throw new KeyNotFoundException("Group chat not found");

        if (!groupChat.Participants.Contains(userId))
            throw new UnauthorizedAccessException("User is not a participant of this group chat");

        await Groups.AddToGroupAsync(Context.ConnectionId, groupChatId.ToString());
        await Clients.Group(groupChatId.ToString()).SendAsync("OnConnected", $"User connected to chat {userId}");
    }

    public async Task SendMessage(Guid groupChatId, string message)
    {
        var userId = _userService.UserId;
        if (userId is null)
            throw new UnauthorizedAccessException();

        var groupChat = await _context.GroupChats.FindAsync(groupChatId);
        if (groupChat is null)
            throw new KeyNotFoundException("Group chat not found");

        if (!groupChat.Participants.Contains(userId))
            throw new UnauthorizedAccessException("User is not a participant of this group chat");

        await Clients.Group(groupChatId.ToString()).SendAsync("OnMessageReceived", userId.ToString(), message);
    }
}