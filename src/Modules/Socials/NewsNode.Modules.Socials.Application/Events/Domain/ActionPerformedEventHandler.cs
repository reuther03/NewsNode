using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Shared.Abstractions.Events.Domain.Posts;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Events;

namespace NewsNode.Modules.Socials.Application.Events.Domain;

public class ActionPerformedEventHandler : IDomainEventHandler<ActionPerformedEvent>
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActionPerformedEventHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ActionPerformedEvent notification, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostByIdAsync(notification.PostId, cancellationToken);
        NullValidator.ValidateNotNull(post);

        post.PerformAction(notification.ActionType);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}