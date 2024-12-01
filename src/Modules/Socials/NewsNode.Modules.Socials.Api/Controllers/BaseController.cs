using Microsoft.AspNetCore.Mvc;

namespace NewsNode.Modules.Socials.Api.Controllers;

[ApiController]
[Route(SocialsModule.BasePath + "/[controller]")]
internal abstract class BaseController : ControllerBase
{
}