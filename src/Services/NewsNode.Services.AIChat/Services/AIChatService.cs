using System.Text.Json;
using Microsoft.AspNetCore.Http;
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
                          !!! If the text is very short (under 10 words), return at most 2 hashtags. 
                          !!! If you’re unsure, return only the most relevant single hashtag. 
                          !!! Do not introduce new topics that are not present in the text.
                          
                         
                          !!!! MOST IMPORTANT KEEP THEM IN SAME LANGUAGE AS INPUT TEXT !!!!
                          !!!! MOST IMPORTANT KEEP THEM IN SAME LANGUAGE AS INPUT TEXT !!!!
                          !!!! like this CONTENT: "Powiedzcie co myślicie o rządzie i sejmie w Polsce"
                                         HASHTAGS: "#sejm #rząd #polityka #Polska"
                                         
                                        CONTENT: "Tell me what you think about the government and the parliament in Poland"
                                        HASHTAGS: "#parliament #government #politics #Poland"
                                        
                                        CONTENT: "Sagt mir, was ihr über die Regierung und das Parlament in Polen denkt"
                                        HASHTAGS: "#Parlament #Regierung #Politik #Polen"
                      """;

        var chatCompletion = await client.CompleteAsync(prompt, cancellationToken: cancellationToken);
        return chatCompletion.ToString();
    }

    public async Task<string> GenerateHashtagsByImage(IFormFile img, string responseLanguage, CancellationToken cancellationToken = default)
    {
        var chatClient = _provider.GetRequiredKeyedService<IChatClient>("llama3.2-vision");

        using var memoryStream = new MemoryStream();
        await img.CopyToAsync(memoryStream, cancellationToken);

        var imgBytes = memoryStream.ToArray();

        var imageContent = new ImageContent(imgBytes);

        var prompt = new TextContent($"""
                                      Please based on the post image, generate up to 5 hashtags and keep in mind that the hashtags should be relevant to the image.
                                      Keep the hashtags in the same language as requested language.
                                      Language: {responseLanguage}
                                      !!! important only the hashtags, no extra text, no spaces, no newlines, no brackets, no quotes, just the hashtags.
                                      !!! return only hashtags without any extra text and your response, just plain hashtags.
                                      !!! keep the hashtags in generalized topics, so if you get image:
                                      of a dog you should return "#animals", "dogs" and try find breed etc.
                                      of cars you should return "#cars", "motorization", "vehicles" and try find what brand is the car etc. 
                                      because it can be important etc.
                                      !!! do not write anything else, just the hashtags.
                                      !!! if the image is very simple, return at most 2 hashtags.
                                      !!! If you’re unsure, return only the most relevant single hashtag. 
                                      !!! Do not introduce new topics that are not present in the text.
                                                 
                                      !!!! MOST IMPORTANT KEEP THEM IN SAME LANGUAGE AS REQUESTED ONE {responseLanguage} !!!!
                                      !!!! MOST IMPORTANT KEEP THEM IN SAME LANGUAGE AS REQUESTED ONE {responseLanguage} !!!!
                                      !!!! like this CONTENT:
                                          "Some Img of a dog" responseLanguage: "Poland"
                                          HASHTAGS: "#zwierze #psy #polska"
                                                                 
                                         CONTENT: "Some Img of a car" responseLanguage: "England"
                                         HASHTAGS: "#cars #vehicles #england"
                                                                
                                         CONTENT: "Some Img of a cat" responseLanguage: "Germany"
                                         HASHTAGS: "#katze #tiere #deutschland"
                                         
                                      !!!! MAX 5 HASHTAGS THE MOST RELEVENT !!!!
                                      

                                      """);

        var message = new ChatMessage(ChatRole.User, [prompt, imageContent]);

        var chatCompletion = await chatClient.CompleteAsync([message], cancellationToken: cancellationToken);

        return chatCompletion.ToString();
    }
}