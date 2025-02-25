using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.CreatePost;

public record CreatePostCommand(
    string Content,
    List<Hashtag?> Hashtags,
    IFormFile Img) : ICommand<Guid>
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
        private readonly IAiChatService _aiChatService;
        private readonly IImgUploader _imgUploader;

        public Handler
        (
            IPostRepository postRepository,
            IUserProfileRepository userProfileRepository,
            IFollowerRepository followerRepository,
            IUserService userService,
            INotificationService notificationService,
            IRecommendationsService recommendationsService,
            IUnitOfWork unitOfWork, IAiChatService aiChatService,
            IImgUploader imgUploader)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
            _followerRepository = followerRepository;
            _userService = userService;
            _notificationService = notificationService;
            _recommendationsService = recommendationsService;
            _unitOfWork = unitOfWork;
            _aiChatService = aiChatService;
            _imgUploader = imgUploader;
        }

        public async Task<Result<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetByIdAsync(_userService.UserId!, cancellationToken);
            NullValidator.ValidateNotNull(userProfile);

            List<Hashtag> hashtags;
            if (request.Hashtags.Count == 0)
            {
                var response = await _aiChatService.GenerateHashtags(request.Content, cancellationToken);
                var tokens = response.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                hashtags = tokens.Where(x => x.StartsWith('#')).Select(x => new Hashtag(x)).ToList();
            }
            else
            {
                hashtags = request.Hashtags!;
            }

            var imgUrl = await _imgUploader.UploadImg(request.Img);
            var postImg = PostImg.Create(imgUrl, request.Img.FileName);

            var post = Post.Create(request.Content, hashtags, userProfile.Id, postImg);

            await _postRepository.AddAsync(post, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            var unMutedFollowers = await _followerRepository.GetFollowersWhereUnMutedAsync(userProfile.Id, cancellationToken);
            await _notificationService.PostNotification(unMutedFollowers, userProfile.Id, post.Id);
            await _recommendationsService.CreateActionRecommendation(userProfile.Id, post.Hashtags.ToList(), cancellationToken);
            await _recommendationsService.CreateCountryRecommendation(userProfile.Location.Country, post.Hashtags.ToList(), cancellationToken);
            await _recommendationsService.IncrementActionRecommendation(userProfile.Id, post.Hashtags.ToList(), PostActionType.Created, cancellationToken);
            await _recommendationsService.IncrementCountryRecommendation(userProfile.Location.Country, post.Hashtags.ToList(), PostActionType.Created,
                cancellationToken);

            return Result<Guid>.Ok(post.Id);
        }
    }
}