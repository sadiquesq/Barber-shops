namespace Barber_shops.CloudinaryServies
{
    public interface ICloudinaryServices
    {
        Task<string> UploadDocumentAsync(IFormFile file);

    }
}
