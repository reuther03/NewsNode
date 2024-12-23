using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.RepostPost;

public record RepostPostCommand(Guid PostId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<RepostPostCommand, Guid>
    {
        private readonly IPostRepository _postRepository;

        public Task<Result<Guid>> Handle(RepostPostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}