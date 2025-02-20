using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.AIChat.Llms;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.AIChat.Services;

public class AIChatService : IAIChatService
{
    private readonly IServiceProvider _provider;

    public AIChatService(IServiceProvider provider)
    {
        _provider = provider;
    }


    public async Task<string> GetRecommendedHashtags(Dictionary<Hashtag, RecommendationWeight> recommendationWeights,
        CancellationToken cancellationToken = default)
    {
        var client = _provider.GetRequiredKeyedService<IChatClient>("llama3");

        var request = string.Join(", ", recommendationWeights.Select(x => x.Key.Value + " " + x.Value));
        var chatMessage = $"""
                            Pick from this dictionary those names where RecommendationWeight is the biggest, you should return 5 names

                            Here i provice RecommendationWeight enum:

                            LowNegative = -1,
                            MediumNegative = -2,
                            HighNegative = -3,
                            VeryHighNegative = -4,
                            None = 0,
                            Low = 1,
                            MediumLow = 2,
                            Medium = 3,
                            MediumHigh = 4,
                            High = 5,
                            VeryHigh = 6

                            important think pick from this list 5 objects with biggest number where number of RecommendationWeight is equal to 0 or bigger

                            return those names in string format
                            :{request}
                            """;

        var chatCompletion = await client.CompleteAsync(chatMessage, cancellationToken: cancellationToken);

        return chatCompletion.ToString();
    }
}