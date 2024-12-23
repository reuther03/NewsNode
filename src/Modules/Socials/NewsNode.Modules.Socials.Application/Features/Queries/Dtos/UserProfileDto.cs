namespace NewsNode.Modules.Socials.Application.Features.Queries.Dtos;

public class UserProfileDto
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public int? FollowersCount { get; init; }
    public int? FollowingCount { get; init; }
    public List<Guid> RepostedPosts { get; init; } = [];
}