using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NewsNode.Shared.Application.Modules;

public interface IModule
{
    string Name { get; }
    string Path { get; }
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);
}