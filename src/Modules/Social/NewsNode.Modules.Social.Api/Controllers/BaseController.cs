using Microsoft.AspNetCore.Mvc;

namespace NewsNode.Modules.Social.Api.Controllers;

[ApiController]
[Route(SocialsModule.BasePath + "/[controller]")]
internal abstract class BaseController : ControllerBase
{
}