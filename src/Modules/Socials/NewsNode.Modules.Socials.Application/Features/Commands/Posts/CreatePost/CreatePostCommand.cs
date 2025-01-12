using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.CreatePost;

public record CreatePostCommand(string Content, List<Hashtag> Hashtags) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<CreatePostCommand, Guid>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IFollowerRepository _followerRepository;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IRecommendationsService _recommendationsService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler
        (
            IPostRepository postRepository,
            IUserProfileRepository userProfileRepository,
            IFollowerRepository followerRepository,
            IUserService userService,
            INotificationService notificationService,
            IRecommendationsService recommendationsService,
            IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
            _followerRepository = followerRepository;
            _userService = userService;
            _notificationService = notificationService;
            _recommendationsService = recommendationsService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(userProfile);

            var post = Post.Create(request.Content, request.Hashtags, userProfile.Id);

            await _postRepository.AddAsync(post, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            var unMutedFollowers = await _followerRepository.GetFollowersWhereUnMutedAsync(userProfile.Id, cancellationToken);
            await _notificationService.PostNotification(unMutedFollowers, userProfile.Id, post.Id);

            return Result<Guid>.Ok(post.Id);
        }
    }
}