using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Services.GroupChats.Database;

namespace NewsNode.Services.GroupChats.Controllers;

internal class GroupChatController : BaseController
{
    private readonly ISender _sender;

    public GroupChatController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult> CreateGroupChat([FromBody] CreateGroupChatRequest command)
    {
        await _sender.Send(command);
        return Ok();
    }
}

public record CreateGroupChatRequest(string Name, string Description);