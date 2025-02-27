using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Shared.Infrastructure.Services.CloudinaryService;

public class ImgUploader : IImgUploader
{
    private readonly Cloudinary _cloudinary;
    private readonly HttpClient _client;

    public ImgUploader(IOptions<CloudinaryOptions> options, IHttpClientFactory httpClientFactory)
    {
        var account = new Account(
            options.Value.cloud_name,
            options.Value.api_key,
            options.Value.api_secret
        );

        _cloudinary = new Cloudinary(account);
        _client = httpClientFactory.CreateClient();
    }

    public async Task<string> UploadImg(IFormFile file)
    {
        if (file.Length is <= 0 or > 5 * 1024 * 1024) // 5 MB limit
            throw new ArgumentException("Invalid file size");

        List<string> validTypes = ["image/jpeg", "image/png", "image/jpg", "image/gif"];
        if (!validTypes.Contains(file.ContentType))
            throw new ArgumentException("Invalid file type");

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = file.FileName
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.Url.ToString();
    }

    public void DeleteImg(string publicId)
    {
        var deleteParams = new DelResParams
        {
            PublicIds = [publicId],
            Type = "upload",
            ResourceType = ResourceType.Image
        };

        _cloudinary.DeleteResources(deleteParams);
    }

    public async Task<byte[]> DownloadImgAsync(string imgUrl)
    {
        var response = await _client.GetAsync(imgUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }
}