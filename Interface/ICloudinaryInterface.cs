namespace Server.Interface
{
    public interface ICloudinaryInterface
    {
        Task<string> UploadImageAsync();
    }
}
