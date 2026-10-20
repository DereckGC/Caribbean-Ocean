using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.AboutUs
{
    public class UpdateAboutUs(IPageContentRepository pageContentRepository)
    {
        public async Task<bool> ExecuteAsync(AdminAboutUsDTO dto)
        {
            var existing = await pageContentRepository.GetPageContentByTitle("about_us");

            if (existing == null)
                throw new NotFoundException("AboutUs content not found.");

            existing.title = dto.Title;
            existing.subtitle = dto.SubTitle;
            existing.description = dto.Description;

            return await pageContentRepository.UpdatePageContent(existing);
        }
    }
}