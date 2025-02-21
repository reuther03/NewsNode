using System.Text.Json;
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

        var xd = recommendationWeights.Select(x => x.Key.Value + " " + x.Value);

        var data = recommendationWeights
            .ToDictionary(kvp => kvp.Key.Value, kvp => kvp.Value); // HashTag -> int

        // Turn it into JSON so the model can parse it more easily:
        var jsonData = JsonSerializer.Serialize(data);

        // Construct a more explicit prompt:
        var prompt = $"""
                          Below is a JSON dictionary of hashtags and their integer “RecommendationWeight”:
                          {jsonData}
                      
                          Please find the 5 hashtags with the biggest RecommendationWeight (only those >= 0), 
                          and return them as a comma-separated list with no extra text.
                          !!! important only the hashtags, no extra text, no spaces, no newlines, no brackets, no quotes, just the hashtags separated by commas.
                          !!! do not write anything else, just the hashtags separated by commas.
                      """;

        var chatCompletion = await client.CompleteAsync(prompt, cancellationToken: cancellationToken);
        return chatCompletion.ToString();
    }
}