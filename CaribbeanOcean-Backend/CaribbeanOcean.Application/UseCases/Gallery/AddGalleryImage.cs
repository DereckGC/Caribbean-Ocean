// CaribbeanOcean.Application/UseCases/Gallery/AddGalleryImage.cs
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CaribbeanOcean.Application.UseCases.Gallery
{
    public class AddGalleryImage(
        IPageContentRepository pageContentRepository,
        ICloudinaryService cloudinaryService)
    {
        public async Task ExecuteAsync(IFormFile image, string alt, int adminId)
        {
            var entity = await pageContentRepository.GetPageContentByTitle("gallery")
                ?? throw new NotFoundException("Gallery content not found.");

            var upload = await cloudinaryService.UploadAsync(image, "gallery");

            var nextId = entity.url_content.Any()
                ? entity.url_content.Max(x => x.id) + 1
                : 1;

            entity.url_content.Add(new()
            {
                id = nextId,
                url = upload.Url,
                alt = alt,
                publicID = upload.PublicId
            });

            await pageContentRepository.UpdateGalleryContent(entity.url_content, adminId);
        }
    }
}