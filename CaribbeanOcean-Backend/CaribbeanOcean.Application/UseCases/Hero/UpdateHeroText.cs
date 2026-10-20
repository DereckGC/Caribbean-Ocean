using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Hero
{
    public class UpdateHeroText(IPageContentRepository pageContentRepository)
    {
        public async Task ExecuteAsync(UpdateHeroTextDTO dto)
        {
            var hero = await pageContentRepository.GetPageContentByTitle("hero");

            if (hero == null)
                throw new NotFoundException("Hero content not found.");

            hero.title = dto.Title;
            hero.subtitle = dto.Subtitle;
            hero.description = dto.Description;

            await pageContentRepository.UpdatePageContent(hero);
        }
    }
}