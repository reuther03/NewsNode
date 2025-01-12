using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.AddPostAction;

public record AddPostActionCommand(Guid PostId, PostActionType ActionType) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddPostActionCommand, Guid>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IRecommendationsService _recommendationsService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IPostRepository postRepository, IUserProfileRepository userProfileRepository, IRecommendationsService recommendationsService,
            IUserService userService, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
            _recommendationsService = recommendationsService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddPostActionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userProfileRepository.GetFullByIdAsync(_userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(user);

            var post = await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken);
            NullValidator.ValidateNotNull(post);

            if (post.CreatedBy == user.Id)
                return Result.BadRequest<Guid>("You can't add action to your own post");

            user.AddPostAction(post.Id, request.ActionType);

            await _unitOfWork.CommitAsync(cancellationToken);
            await _recommendationsService.IncrementActionRecommendation(user.Id, post.Hashtags.ToList(), request.ActionType, cancellationToken);
            await _recommendationsService.IncrementCountryRecommendation(user.Location.Country, post.Hashtags.ToList(), request.ActionType, cancellationToken);


            return Result.Ok<Guid>(post.Id);
        }
    }
}