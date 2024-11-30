using Microsoft.AspNetCore.Mvc;

namespace NewsNode.Modules.Social.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("Social API");
}