using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Modules.Socials.Application.Features.Commands.Posts.CreatePost;
using NewsNode.Modules.Socials.Application.Features.Commands.Posts.RepostPost;

namespace NewsNode.Modules.Socials.Api.Controllers;

internal class PostController : BaseController
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize]
    public async  Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPost("repost")]
    [Authorize]
    public async Task<IActionResult> RepostPost([FromBody] RepostPostCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

}