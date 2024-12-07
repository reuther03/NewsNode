using MediatR;

namespace NewsNode.Modules.Socials.Api.Controllers;

internal class PostController : BaseController
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }
}