using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Server.Application.Interface;

namespace Server.Application.Service
{
    public class CloudinaryService : ICloudinaryInterface
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudinaryUrl = configuration["Cloudinary:CloudinaryUrl"];
            _cloudinary = new Cloudinary(cloudinaryUrl);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty");

                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true,
                    Folder = "cloud_storage/images"
                };

                var result = await _cloudinary.UploadAsync(uploadParams);
                if (result.Error != null)
                    throw new InvalidOperationException("Upload failed: " + result.Error.Message);

                return result.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Upload failed", ex);
            }
        }




        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty");

                await using var stream = file.OpenReadStream();

                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true,
                    Folder = "cloud_storage/videos"
                };

                var result = await _cloudinary.UploadAsync(uploadParams);
                if (result.Error != null)
                    throw new InvalidOperationException("Upload failed: " + result.Error.Message);

                return result.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Video upload failed", ex);
            }
        }

    }
}
