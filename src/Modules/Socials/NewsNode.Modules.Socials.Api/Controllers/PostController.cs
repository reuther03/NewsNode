using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Modules.Socials.Application.Features.Commands.Posts.AddPostAction;
using NewsNode.Modules.Socials.Application.Features.Commands.Posts.CreatePost;
using NewsNode.Modules.Socials.Application.Features.Queries.Posts;

namespace NewsNode.Modules.Socials.Api.Controllers;

internal class PostController : BaseController
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("followers/posts")]
    [Authorize]
    public async Task<IActionResult> GetFollowersPosts([FromQuery] GetFollowersPosts query)
    {
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPost("addAction")]
    [Authorize]
    public async Task<IActionResult> AddAction([FromBody] AddPostActionCommand actionCommand)
    {
        var result = await _sender.Send(actionCommand);
        return Ok(result);
    }
}