using Microsoft.AspNetCore.Http;

namespace NewsNode.Shared.Abstractions.Services;

public interface IImgUploader
{
    Task<string> UploadImg(IFormFile file);
    void DeleteImg(string publicId);
}