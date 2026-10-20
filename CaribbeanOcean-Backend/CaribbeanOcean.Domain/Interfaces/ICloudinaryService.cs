using Microsoft.AspNetCore.Http;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(
            IFormFile file,
            string folder = "room-types"
        );

        Task<(string Url, string PublicId)> UploadAsync(
            IFormFile file,
            string folder = "gallery"
        );

        Task DeleteAsync(string publicId);
    }
}