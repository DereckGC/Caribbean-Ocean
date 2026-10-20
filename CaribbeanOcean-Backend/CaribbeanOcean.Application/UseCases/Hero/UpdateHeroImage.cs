using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Hero
{
    public class UpdateHeroImage(IPageContentRepository pageContentRepository)
    {
        public async Task ExecuteAsync(int imageId, UpdateHeroImageDTO dto)
        {
            var hero = await pageContentRepository.GetPageContentByTitle("hero");

            if (hero == null)
                throw new NotFoundException("Hero content not found.");

            var image = hero.url_content.FirstOrDefault(i => i.id == imageId);

            if (image == null)
                throw new NotFoundException($"Image with id {imageId} not found.");

            image.url = dto.ImageUrl ?? image.url;
            image.alt = dto.Alt;

            await pageContentRepository.UpdatePageContent(hero);
        }
    }
}