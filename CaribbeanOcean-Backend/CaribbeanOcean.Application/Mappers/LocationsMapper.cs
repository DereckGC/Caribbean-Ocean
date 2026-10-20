using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.DTOs.Multimedia;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.Mappers
{
    internal class LocationsMapper
    {
        public static LocationsDTO ToDTO(PageContent entity)
        {
            if (entity == null) return null!;
            return new LocationsDTO
            {
                Title = entity.title,
                Subtitle = entity.subtitle,
                Description = entity.description,
                Images = entity.url_content.Select(content => MultimediaMapper.ToDTO(content)).ToList()
            };
        }

        public static PageContent ToEntity(LocationsDTO dto)
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
