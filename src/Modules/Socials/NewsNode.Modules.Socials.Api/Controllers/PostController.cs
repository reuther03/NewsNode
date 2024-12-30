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

    [HttpGet("{postId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetPosts([FromRoute] Guid postId)
    {
        var result = await _sender.Send(new GetPostQuery(postId));
        return Ok(result);
    }

    [HttpGet("trending")]
    public async Task<IActionResult> GetTrendingPosts([FromQuery] GetTrendingPosts query)
    {
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("followers/posts")]
    [Authorize]
    public async Task<IActionResult> GetFollowersPosts([FromQuery] GetFollowersPostsQuery query)
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