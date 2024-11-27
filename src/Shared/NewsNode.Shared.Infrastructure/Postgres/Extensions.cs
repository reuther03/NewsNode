using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Application.Kernel.Database;
using NewsNode.Shared.Application.QueriesAndCommands.Commands;
using NewsNode.Shared.Infrastructure.Postgres.Decorators;

namespace NewsNode.Shared.Infrastructure.Postgres;

public static class Extensions
{
    internal static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddSingleton(options);
        services.AddSingleton(new UnitOfWorkTypeRegistry());

        return services;
    }

    public static IServiceCollection AddDecorators(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<,>), typeof(TransactionalCommandHandlerDecorator<>));

        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));
        return services;
    }

    public static IServiceCollection AddUnitOfWork<TUnitOfWork, TImplementation>(this IServiceCollection services)
        where TUnitOfWork : class, IBaseUnitOfWork where TImplementation : class, TUnitOfWork
    {
        services.AddScoped<TUnitOfWork, TImplementation>();
        services.AddScoped<IBaseUnitOfWork, TImplementation>();

        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<TUnitOfWork>();

        return services;
    }
}