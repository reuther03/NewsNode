using MediatR;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database;

internal class UnitOfWork : BaseUnitOfWork<SocialsDbContext>, IUnitOfWork
{
    public UnitOfWork(SocialsDbContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}