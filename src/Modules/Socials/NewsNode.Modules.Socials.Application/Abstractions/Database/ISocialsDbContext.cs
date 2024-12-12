using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Domain.Article;
using NewsNode.Modules.Socials.Domain.UserProfile;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface ISocialsDbContext
{
    DbSet<UserProfile> UserProfiles { get; }
    DbSet<UserProfileRelation> UserProfileFollowers { get; }
    DbSet<Post> Posts { get; }
}