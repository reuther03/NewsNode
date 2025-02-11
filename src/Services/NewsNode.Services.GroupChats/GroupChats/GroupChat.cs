using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.GroupChats.GroupChats;

public class GroupChat : Entity<Guid>
{
    private readonly List<UserId> _participants = [];

    public Name Name { get; private set; }
    public string Description { get; private set; }
    public IList<Hashtag> Hashtags { get; private set; }

    public IReadOnlyList<UserId> Participants => _participants;

    private GroupChat()
    {
    }

    private GroupChat(Guid id, Name name, string description, IList<Hashtag> hashtags) : base(id)
    {
        Name = name;
        Description = description;
        Hashtags = hashtags;
    }

    public static GroupChat Create(Name name, string description, IList<Hashtag> hashtags)
        => new(Guid.NewGuid(), name, description, hashtags);

    public void AddParticipant(UserId userId)
    {
        if (_participants.Contains(userId))
            throw new InvalidOperationException("User is already a participant");

        _participants.Add(userId);
    }
}