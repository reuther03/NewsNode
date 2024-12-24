using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.RepostPost;

public record RepostPostCommand(Guid PostId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<RepostPostCommand, Guid>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IPostRepository postRepository, IUserProfileRepository userProfileRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RepostPostCommand request, CancellationToken cancellationToken)
        {
            var user = await _userProfileRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(user);

            var post = await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken);
            NullValidator.ValidateNotNull(post);

            if (post.CreatedBy == user.Id)
                return Result.BadRequest<Guid>("You can't repost your own post");

            user.AddPostAction(post.Id, PostActionType.Reposted);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok<Guid>(post.Id);
        }
    }
}