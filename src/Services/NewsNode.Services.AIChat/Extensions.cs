using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.AIChat.Services;
using NewsNode.Shared.Abstractions.Services;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Services.AIChat;

public static class Extensions
{
    public static IServiceCollection AddAIChat(this IServiceCollection services)
    {
        services.AddKeyedChatClient("llama3", new OllamaChatClient(new Uri("http://localhost:11434"), "llama3"));
        services.AddSingleton<IAiChatService, AiChatService>();
        return services;
    }
}