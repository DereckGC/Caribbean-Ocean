// CaribbeanOcean.Application/UseCases/Gallery/UpdateGalleryImage.cs
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CaribbeanOcean.Application.UseCases.Gallery
{
    public class UpdateGalleryImage(
        IPageContentRepository pageContentRepository,
        ICloudinaryService cloudinaryService)
    {
        public async Task ExecuteAsync(int imageId, IFormFile newImage, string newAlt, int adminId)
        {
            var entity = await pageContentRepository.GetPageContentByTitle("gallery")
                ?? throw new NotFoundException("Gallery content not found.");

            var image = entity.url_content.FirstOrDefault(x => x.id == imageId)
                ?? throw new NotFoundException("Image not found.");

            // Borrar la imagen anterior en Cloudinary
            if (!string.IsNullOrEmpty(image.publicID))
                await cloudinaryService.DeleteAsync(image.publicID);

            // Subir la nueva imagen
            var upload = await cloudinaryService.UploadAsync(newImage, "gallery");

            // Actualizar los datos en la entidad
            image.url = upload.Url;
            image.publicID = upload.PublicId;
            image.alt = newAlt;

            await pageContentRepository.UpdateGalleryContent(entity.url_content, adminId);
        }
    }
}