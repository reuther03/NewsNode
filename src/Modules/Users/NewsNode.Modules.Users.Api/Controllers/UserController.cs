using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsNode.Modules.Users.Application.Features.Commands.Login;
using NewsNode.Modules.Users.Application.Features.Commands.Register;

namespace NewsNode.Modules.Users.Api.Controllers;

internal class UserController : BaseController
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterCommand request)
    {
        var result = await _sender.Send(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _sender.Send(request);
        return Ok(result);
    }
}