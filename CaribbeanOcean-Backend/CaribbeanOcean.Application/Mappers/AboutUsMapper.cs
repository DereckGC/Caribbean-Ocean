using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.Mappers
{
    public class AboutUsMapper
    {
        public static AboutUsDTO ToDTO(PageContent entity)
        {
            if (entity == null) return null!;
            return new AboutUsDTO
            {
                Title = entity.title,
                Subtitle = entity.subtitle,
                Description = entity.description
            };
        }

        public static PageContent ToEntity(AboutUsDTO dto)
        {
            if (dto == null) return null!;
            return new PageContent
            {
                title = dto.Title,
                subtitle = dto.Subtitle,
                description = dto.Description
            };
        }

    }
}
