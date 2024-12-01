using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Modules.Socials.Application.Features.Commands.FollowUserProfile;

namespace NewsNode.Modules.Socials.Api.Controllers;

internal class SocialController : BaseController
{
    private readonly ISender _sender;

    public SocialController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("{userProfileId:guid}/follow")]
    [Authorize]
    public async Task<IActionResult> FollowUserProfile([FromRoute] Guid userProfileId)
    {
        var result = await _sender.Send(new FollowUserProfileCommand(userProfileId));
        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Test()
    {
        return Ok("Test");
    }
}