using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Barber_shops.CloudinaryServies
{

        public class CloudinaryServices :ICloudinaryServices
        {


            private readonly Cloudinary _cloudinary;
            private readonly ILogger<CloudinaryServices> _logger;

        public CloudinaryServices(IConfiguration configuration, ILogger<CloudinaryServices> logger)
        {
                _logger = logger;

                var cloudName = configuration["Cloudinary:CloudName"];
                var apiKey = configuration["Cloudinary:ApiKey"];
                var apiSecret = configuration["Cloudinary:ApiSecret"];

                if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                {
                    throw new Exception("Cloudinary configuration is missing or incomplete");
                }

                var account = new Account(cloudName, apiKey, apiSecret);
                _cloudinary = new Cloudinary(account);
            }

            public async Task<string> UploadDocumentAsync(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogError("File is null or empty.");
                    return null;
                }

                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(300).Width(500).Crop("fill")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Error != null)
                    {
                        _logger.LogError($"Cloudinary upload error: {uploadResult.Error.Message}");
                        throw new Exception($"Cloudinary upload error: {uploadResult.Error.Message}");
                    }

                    _logger.LogInformation($"Cloudinary upload successful. URL: {uploadResult.SecureUrl}");
                    return uploadResult.SecureUrl?.ToString();
                }
            }
        }
}
