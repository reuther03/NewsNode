// using System.Text.Json.Serialization;
// using Microsoft.AspNetCore.Http;
// using NewsNode.Modules.Socials.Application.Abstractions;
// using NewsNode.Modules.Socials.Application.Abstractions.Database;
// using NewsNode.Modules.Socials.Domain.Post;
// using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
// using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
// using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
// using NewsNode.Shared.Abstractions.Services;
//
// namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.AddPostComment;
//
// public record AddPostCommentCommand(
//     [property: JsonIgnore]
//     Guid PostId,
//     string Content,
//     IFormFile Img) : ICommand<Guid>
// {
//     internal sealed class Handler : ICommandHandler<AddPostCommentCommand, Guid>
//     {
//         private readonly IPostRepository _postRepository;
//         private readonly IUserProfileRepository _userProfileRepository;
//         private readonly IUserService _userService;
//         private readonly IImgUploader _imgUploader;
//         private readonly IUnitOfWork _unitOfWork;
//
//         public Handler
//         (
//             IPostRepository postRepository,
//             IUserProfileRepository userProfileRepository,
//             IUserService userService,
//             IImgUploader imgUploader,
//             IUnitOfWork unitOfWork)
//         {
//             _postRepository = postRepository;
//             _userProfileRepository = userProfileRepository;
//             _userService = userService;
//             _imgUploader = imgUploader;
//             _unitOfWork = unitOfWork;
//         }
//
//
//         public async Task<Result<Guid>> Handle(AddPostCommentCommand request, CancellationToken cancellationToken)
//         {
//             var user = await _userProfileRepository.GetByIdAsync(_userService.UserId!, cancellationToken);
//             NullValidator.ValidateNotNull(user);
//
//             var post = await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken);
//             NullValidator.ValidateNotNull(post);
//
//             var imgUrl = await _imgUploader.UploadImg(request.Img);
//             var contentImg = ContentImg.Create(imgUrl, request.Img.FileName);
//
//             var comment = Comment.Create(request.Content, user.Id, contentImg);
//
//             post.AddComment(comment);
//
//             await _unitOfWork.CommitAsync(cancellationToken);
//
//             return Result<Guid>.Ok(comment.Id);
//         }
//     }
// }