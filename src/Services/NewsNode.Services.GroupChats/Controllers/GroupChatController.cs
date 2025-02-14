using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsNode.Services.GroupChats.Database;
using NewsNode.Services.GroupChats.Dtos;
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
    private readonly IRedisCacheService _redisCacheService;


    public GroupChatController(GroupChatsDbContext context, IUserService userService, IRedisCacheService redisCacheService)
    {
        _context = context;
        _userService = userService;
        _redisCacheService = redisCacheService;
    }

    [HttpGet("{groupChatId:guid}")]
    public async Task<ActionResult> GetGroupChat([FromRoute] Guid groupChatId)
    {
        var groupChat = await _context.GroupChats.FindAsync(groupChatId);
        if (groupChat == null)
            throw new BadHttpRequestException("Group chat not found");

        var chatMessages = await _context.ChatMessages
            .Where(x => x.GroupChatId == groupChatId)
            .OrderByDescending(x => x.SentAt)
            .Take(50)
            .Select(x => ChatMessageDto.AsDto(
                x,
                _context.GroupUsers.Where(y => y.UserId == x.SenderId).Select(z => z.UserName).FirstOrDefault()!)
            )
            .ToListAsync();

        return Ok(Result.Ok(chatMessages));
    }

    [HttpPost]
    public async Task<ActionResult> CreateGroupChat([FromBody] CreateGroupChatRequest request)
    {
        if (!_userService.IsAuthenticated)
            throw new UnauthorizedAccessException();

        var groupChat = GroupChat.Create(new Name(request.Name), request.Description, request.Hashtags);
        var user = GroupUser.Create(_userService.UserId, _userService.UserName, GroupUserRole.Admin, groupChat.Id);

        await _context.GroupUsers.AddAsync(user);
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

        var user = GroupUser.Create(_userService.UserId, _userService.UserName, GroupUserRole.Member, groupChat.Id);

        await _context.GroupUsers.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok(Result.Ok());
    }
}