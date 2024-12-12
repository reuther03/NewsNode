using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Modules.Socials.Application.Features.Commands.UserProfiles.FollowUserProfile;
using NewsNode.Modules.Socials.Application.Features.Commands.UserProfiles.MuteUserProfile;
using NewsNode.Modules.Socials.Application.Features.Queries.UserProfile;

namespace NewsNode.Modules.Socials.Api.Controllers;

internal class UserProfileController : BaseController
{
    private readonly ISender _sender;

    public UserProfileController(ISender sender)
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

    [HttpPatch("{userProfileId:guid}/relation-status")]
    [Authorize]
    public async Task<IActionResult> MuteUserProfile([FromBody] AddUserProfileRelationStatusCommand command, [FromRoute] Guid userProfileId)
    {
        var result = await _sender.Send(command with { UserProfileId = userProfileId });
        return Ok(result);
    }
}