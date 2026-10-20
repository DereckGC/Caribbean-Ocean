using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Gallery;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.DTOs.Multimedia;
using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Mappers
{
    internal class GalleryMapper
    {
        public static GalleryDTO ToDTO(PageContent entity)
        {
            if (entity == null) return null!;
            return new GalleryDTO
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

        public static MultimediaDTO ToDTO(MultimediaContent m) => new()
        {
            Id = m.id,
            URL = m.url,
            Alt = m.alt,
            PublicId = m.publicID
        };

        public static MultimediaContent ToEntity(MultimediaDTO dto) => new()
        {
            id = dto.Id,
            url = dto.URL,
            alt = dto.Alt,
            publicID = dto.PublicId
        };
    }
}
