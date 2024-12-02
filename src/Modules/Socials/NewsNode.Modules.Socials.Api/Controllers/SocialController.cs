using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Modules.Socials.Application.Features.Commands.FollowUserProfile;
using NewsNode.Modules.Socials.Application.Features.Queries.UserProfile;

namespace NewsNode.Modules.Socials.Api.Controllers;

internal class SocialController : BaseController
{
    private readonly ISender _sender;

    public SocialController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{userProfileId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUserProfile([FromRoute] Guid userProfileId)
    {
        var result = await _sender.Send(new GetUserProfileQuery(userProfileId));
        return Ok(result);
    }

    [HttpPost("{userProfileId:guid}/follow")]
    [Authorize]
    public async Task<IActionResult> FollowUserProfile([FromRoute] Guid userProfileId)
    {
        var result = await _sender.Send(new FollowUserProfileCommand(userProfileId));
        return Ok(result);
    }
}