using CaribbeanOcean.Application.DTOs.Multimedia;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Hero
{
    public class GetHeroImages(IPageContentRepository pageContentRepository)
    {
        public async Task<IEnumerable<MultimediaDTO>> ExecuteAsync()
        {
            var hero = await pageContentRepository.GetPageContentByTitle("hero");

            if (hero == null)
                throw new NotFoundException("Hero content not found.");

            return hero.url_content.Select(MultimediaMapper.ToDTO);
        }
    }
}