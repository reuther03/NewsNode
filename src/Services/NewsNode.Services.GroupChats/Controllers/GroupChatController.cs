using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Services.GroupChats.Database;
using NewsNode.Services.GroupChats.GroupChats;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.GroupChats.Controllers;

internal class GroupChatController : BaseController
{
    private readonly ISender _sender;
    private readonly GroupChatsDbContext _context;
    private readonly IUserService _userService;


    public GroupChatController(ISender sender, GroupChatsDbContext context, IUserService userService)
    {
        _sender = sender;
        _context = context;
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateGroupChat([FromBody] CreateGroupChatRequest request)
    {
        if (!_userService.IsAuthenticated)
            throw new UnauthorizedAccessException();

        var groupChat = GroupChat.Create(new Name(request.Name), request.Description, request.Hashtags);
        groupChat.AddParticipant(_userService.UserId);

        await _context.GroupChats.AddAsync(groupChat);
        await _context.SaveChangesAsync();

        return Ok(Result.Ok(groupChat.Id));
    }
}

public record CreateGroupChatRequest
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IList<Hashtag> Hashtags { get; init; } = [];
}