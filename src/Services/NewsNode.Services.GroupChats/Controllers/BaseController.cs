using Microsoft.AspNetCore.Mvc;

namespace NewsNode.Services.GroupChats.Controllers;

[ApiController]
[Route(GroupChatModule.BasePath + "/[controller]")]
internal abstract class BaseController : ControllerBase
{
}