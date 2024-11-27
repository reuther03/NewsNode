using Microsoft.AspNetCore.Mvc;

namespace NewsNode.Modules.Users.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("User API");
}