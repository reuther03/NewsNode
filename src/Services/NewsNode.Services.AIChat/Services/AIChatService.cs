using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.AIChat.Llms;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.AIChat.Services;

public class AIChatService : IAIChatService
{
    private readonly IServiceProvider _provider;

    public AIChatService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<string?> GetRecommendedHashtags(string userId, CancellationToken cancellationToken = default)
    {
        var client = _provider.GetRequiredKeyedService<IChatClient>("llama3");

        var chatCompletion = await client.CompleteAsync(userId);

        return chatCompletion.Message.ToString();
    }
}