using System.Text.Json;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.AIChat.Llms;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.AIChat.Services;

public class AiChatService : IAiChatService
{
    private readonly IServiceProvider _provider;

    public AiChatService(IServiceProvider provider)
    {
        _provider = provider;
    }


    public async Task<string> GenerateHashtags(Dictionary<Hashtag, RecommendationWeight> recommendationWeights,
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

    public async Task<string> GenerateHashtags(string postContent, CancellationToken cancellationToken = default)
    {
        var client = _provider.GetRequiredKeyedService<IChatClient>("llama3");

        var jsonData = JsonSerializer.Serialize(postContent);

        var prompt = $"""
                          Below is a JSON with post content:
                          {jsonData}
                      
                          Please based on the post content, generate up to 5 hashtags and keep in mind that the hashtags should be relevant to the content.
                          Keep the hashtags in the same language as the post content.
                          !!! important only the hashtags, no extra text, no spaces, no newlines, no brackets, no quotes, just the hashtags.
                          !!! return only hashtags without any extra text and your response, just plain hashtags.
                          !!! keep the hashtags in generalized topics, so if you get something:
                           about "dogs" you should return "#animals", "dogs" and try find breed etc.
                           about "programming" you should return "#technology", "programming", "It" or try find about what language is the content 
                           because it can be important etc.
                           about politics try to find what is the main topic, is it about "elections", "president", "parliament"
                           and include country or like european union and try include if its about certain party or politician etc.
                          !!! do not write anything else, just the hashtags.
                          !!!! MOST IMPORTANT KEEP THEM IN SAME LANGUAGE AS INPUT TEXT !!!!
                          !!!! MOST IMPORTANT KEEP THEM IN SAME LANGUAGE AS INPUT TEXT !!!!
                      """;

        var chatCompletion = await client.CompleteAsync(prompt, cancellationToken: cancellationToken);
        return chatCompletion.ToString();
    }
}