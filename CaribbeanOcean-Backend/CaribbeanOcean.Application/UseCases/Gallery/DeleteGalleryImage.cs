// CaribbeanOcean.Application/UseCases/Gallery/DeleteGalleryImage.cs
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Gallery
{
    public class DeleteGalleryImage(
        IPageContentRepository pageContentRepository,
        ICloudinaryService cloudinaryService)
    {
        public async Task ExecuteAsync(int imageId, int adminId)
        {
            var entity = await pageContentRepository.GetPageContentByTitle("gallery")
                ?? throw new NotFoundException("Gallery content not found.");

            var image = entity.url_content.FirstOrDefault(x => x.id == imageId)
                ?? throw new NotFoundException("Image not found.");

            // Borrar físicamente en Cloudinary
            if (!string.IsNullOrEmpty(image.publicID))
                await cloudinaryService.DeleteAsync(image.publicID);

            entity.url_content.Remove(image);

            await pageContentRepository.UpdateGalleryContent(entity.url_content, adminId);
        }
    }
}