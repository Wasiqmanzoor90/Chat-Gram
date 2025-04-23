namespace Server.Application.Interface
{
    public interface ICloudinaryInterface
    {
        Task<string> UploadImageAsync();
    }
}
