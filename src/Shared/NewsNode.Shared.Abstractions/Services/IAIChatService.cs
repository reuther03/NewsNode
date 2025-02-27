using Microsoft.AspNetCore.Http;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Shared.Abstractions.Services;

public interface IAiChatService
{
    Task<string> GenerateHashtags(string postContent, CancellationToken cancellationToken = default);
    Task<string> GenerateHashtagsByImage(IFormFile img, string responseLanguage,CancellationToken cancellationToken = default);
}