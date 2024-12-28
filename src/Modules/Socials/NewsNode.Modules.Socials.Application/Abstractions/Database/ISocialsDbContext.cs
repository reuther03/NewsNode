using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Modules.Socials.Domain.UserProfile;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface ISocialsDbContext
{
    DbSet<UserProfile> UserProfiles { get; }
    DbSet<UserProfileFollow> UserProfileFollowers { get; }
    DbSet<UserProfileStatus> UserProfileStatuses { get; }
    DbSet<PostAction> PostActions { get; }
    DbSet<Post> Posts { get; }
}