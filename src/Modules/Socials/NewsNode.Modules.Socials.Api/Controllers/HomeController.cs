using Microsoft.AspNetCore.Mvc;

namespace NewsNode.Modules.Socials.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("NewsNode Socials API");
}