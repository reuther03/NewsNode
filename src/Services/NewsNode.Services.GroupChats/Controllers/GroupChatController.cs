using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Services.GroupChats.Database;
using NewsNode.Services.GroupChats.GroupChats;
using NewsNode.Services.GroupChats.Requests;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.GroupChats.Controllers;

[Authorize]
internal class GroupChatController : BaseController
{
    private readonly GroupChatsDbContext _context;
    private readonly IUserService _userService;


    public GroupChatController(GroupChatsDbContext context, IUserService userService)
    {
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

    [HttpPost("{groupChatId:guid}/join")]
    public async Task<ActionResult> JoinGroupChat([FromRoute] Guid groupChatId)
    {
        if (!_userService.IsAuthenticated)
            throw new UnauthorizedAccessException();

        var groupChat = await _context.GroupChats.FindAsync(groupChatId);
        if (groupChat == null)
            throw new BadHttpRequestException("Group chat not found");

        groupChat.AddParticipant(_userService.UserId);
        await _context.SaveChangesAsync();

        return Ok(Result.Ok());
    }
}