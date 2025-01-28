using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Pagination;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Extensions;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public record GetFilteredPostsQuery : IQuery<PaginatedList<PostDto>>
{
    public string? SearchValue { get; init; }
    public List<string> Hashtags { get; init; } = [];
    public Guid? PostedBy { get; init; }
    public Location? Location { get; init; }
    public int? PublicationDate { get; init; }

    [property: JsonIgnore]
    public int Page { get; init; } = 1;

    internal sealed class Handler : IQueryHandler<GetFilteredPostsQuery, PaginatedList<PostDto>>
    {
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(ISocialsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PaginatedList<PostDto>>> Handle(GetFilteredPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);

            NullValidator.ValidateNotNull(user);

            //pomyslec czy zmienic lokalizacje tak zeby wyszukiwalo tylko po miesice bo aktualnie miasto musi byc jakiekolwiek
            // i jak sie podaje kraj i miasto to daje wynik tez z samego kraju a nie z obu wiec zawsze po wpisaniu miasta bedzie tez wynik z samego kraju

            var posts = await _dbContext.Posts
                .Where(x => x.CreatedBy != user.Id)
                .WhereIf(!string.IsNullOrWhiteSpace(request.SearchValue), x => EF.Functions.Like(x.Content, $"%{request.SearchValue}%"))
                .WhereIf(request.Hashtags.Count != 0, x => x.Hashtags.Any(y => request.Hashtags.Contains(y.Value)))
                .WhereIf(request.PostedBy.HasValue, x => x.CreatedBy == UserId.From(request.PostedBy!.Value))
                .WhereIf(request.Location != null, x => _dbContext.UserProfiles
                    .Where(y => y.Location.City == request.Location!.City || y.Location.Country == request.Location.Country)
                    .Select(y => y.Id).Contains(x.CreatedBy))
                .WhereIf(request.PublicationDate.HasValue, x => x.PostedAt.Day >= DateTime.UtcNow.Date.Day - request.PublicationDate)
                .ToPagedListAsync<Post, PostDto>(request.Page, 10, x => PostDto.AsDto(x), cancellationToken);

            return Result.Ok(posts);
        }
    }
}