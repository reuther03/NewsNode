using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Pagination;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public record GetRecommendedPostsQuery : IQuery<PaginatedList<PostDto>>
{
    internal sealed class Handler : IQueryHandler<GetRecommendedPostsQuery, PaginatedList<PostDto>>
    {
        private readonly IRecommendationsService _recommendations;
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly ISeenPostService _seenPostService;

        public Handler(IRecommendationsService recommendations, IUserService userService, ISocialsDbContext dbContext, ISeenPostService seenPostService)
        {
            _recommendations = recommendations;
            _userService = userService;
            _dbContext = dbContext;
            _seenPostService = seenPostService;
        }

        public async Task<Result<PaginatedList<PostDto>>> Handle(GetRecommendedPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.UserProfiles
                .Include(x => x.SeenPosts)
                .FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(user);

           //sprawdzic czy napewno recommended profiles nie powinno miec weight dla hashtagow, bo tera zwraca z 0

            var recommendedHashtags = await _recommendations.GetRecommendedHashtags(user.Id, cancellationToken);
            var lessInterestedHashtags = await _recommendations.GetLessInterestedHashtags(user.Id, cancellationToken);
            var recommendedProfiles = await _recommendations.GetRecommendedProfiles(user.Id, cancellationToken);

            //cache na last seen posts po kazdym getcie ma sie zapisywac jako poprzednie i dodawac ja na koniec postow bez powtorzen

            var seenPostsIds = await _dbContext.SeenPosts
                .Where(x => x.UserId == user.Id)
                .Select(x => x.PostId)
                .ToHashSetAsync(cancellationToken);

            var posts = await _dbContext.Posts
                .Where(p => recommendedProfiles.Contains(p.CreatedBy) ||
                    recommendedHashtags.Select(x => x.Key.Value)
                        .Intersect(p.Hashtags.Select(y => y.Value)).Any() &&
                    lessInterestedHashtags.Select(x => x.Key.Value)
                        .Intersect(p.Hashtags.Select(y => y.Value)).Any() &&
                    p.CreatedBy != user.Id &&
                    !_dbContext.UserProfileStatuses
                        .Any(y => y.TargetUserId == p.CreatedBy))
                .ToListAsync(cancellationToken);

            var postsWithWeights = posts.Select(x => new
            {
                Post = x,
                Seen = seenPostsIds.Contains(x.Id),
                Weight = recommendedHashtags.FirstOrDefault(h => x.Hashtags.Contains(h.Key)).Value
            }).ToList();

            var unseenPostsDto = postsWithWeights.Where(p => !p.Seen).Select(p => PostDto.AsDto(p.Post, false, p.Weight)).ToList();
            var seenPostsDto = postsWithWeights.Where(p => p.Seen).Select(p => PostDto.AsDto(p.Post, true, p.Weight)).ToList();

            var allPostsDto = unseenPostsDto
                .Concat(seenPostsDto)
                .OrderBy(x => x.Seen)
                .ThenByDescending(x => x.Weight)
                .ToList();

            await _seenPostService.MarkAsSeenAsync(user.Id, postsWithWeights.Select(x => x.Post.Id).ToList(), cancellationToken);

            return PaginatedList<PostDto>.Create(1, 10, allPostsDto.Count, allPostsDto);
        }
    }
}