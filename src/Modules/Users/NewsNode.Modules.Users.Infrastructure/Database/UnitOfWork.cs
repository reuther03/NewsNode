using MediatR;
using NewsNode.Modules.Users.Application.Abstractions;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Users.Infrastructure.Database;

internal class UnitOfWork : BaseUnitOfWork<UsersDbContext>, IUnitOfWork
{
    public UnitOfWork(UsersDbContext context, IPublisher publisher) : base(context, publisher)
    {
    }
}