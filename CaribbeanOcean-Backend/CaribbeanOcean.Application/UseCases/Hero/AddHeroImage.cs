using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Hero
{
    public class AddHeroImage(IPageContentRepository pageContentRepository)
    {
        public async Task ExecuteAsync(AddHeroImageDTO dto)
        {
            var hero = await pageContentRepository.GetPageContentByTitle("hero");

            if (hero == null)
                throw new NotFoundException("Hero content not found.");

            var newId = hero.url_content.Any()
                ? hero.url_content.Max(i => i.id) + 1
                : 0;

            hero.url_content.Add(new MultimediaContent
            {
                id = newId,
                url = dto.ImageUrl ?? string.Empty,
                alt = dto.Alt
            });

            await pageContentRepository.UpdatePageContent(hero);
        }
    }
}