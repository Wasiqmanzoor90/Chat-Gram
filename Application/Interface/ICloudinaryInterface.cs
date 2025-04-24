namespace Server.Application.Interface
{
    public interface ICloudinaryInterface
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadVideoAsync(IFormFile file);
    }
}
