using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Mappers
{
    public class HeroMapper
    {
        public static HeroDTO ToDTO(PageContent entity)
        {
            if (entity == null) return null!;
            return new HeroDTO
            {
                Title = entity.title,
                Subtitle = entity.subtitle,
                Description = entity.description,
                Images = entity.url_content.Select(content => MultimediaMapper.ToDTO(content)).ToList()
            };
        }

        public static PageContent ToEntity(HeroDTO dto)
        {
            if (dto == null) return null!;
            return new PageContent
            {
                title = dto.Title,
                subtitle = dto.Subtitle,
                description = dto.Description,
                url_content = dto.Images.Select(content => MultimediaMapper.ToEntity(content)).ToList()
            };
        }

    }
}
