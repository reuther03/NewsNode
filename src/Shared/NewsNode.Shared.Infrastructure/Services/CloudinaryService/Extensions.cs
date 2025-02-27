using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Shared.Infrastructure.Services.CloudinaryService;

public static class Extensions
{
    public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudinaryOptions>(configuration.GetRequiredSection(CloudinaryOptions.SectionName));
        services.AddHttpClient<IImgUploader>();
        services.AddSingleton<IImgUploader, ImgUploader>();

        return services;
    }
}