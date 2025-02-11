using Microsoft.AspNetCore.Mvc;

namespace NewsNode.Services.GroupChats.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("GroupChats Service Api");
}