namespace NewsNode.Shared.Infrastructure.Services.CloudinaryService;

public class CloudinaryOptions
{
    internal const string SectionName = "cloudinary";

    public string cloud_name { get; init; }
    public string api_key { get; init; }
    public string api_secret { get; init; }
}