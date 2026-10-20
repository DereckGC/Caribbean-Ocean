using CaribbeanOcean.Application.DTOs.Multimedia;
using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Mappers
{
    public class MultimediaMapper
    {
        public static MultimediaDTO ToDTO(MultimediaContent entity)
        {
            if (entity == null) return null!;
            return new MultimediaDTO
            {
                Id = entity.id,
                URL = entity.url,
                Alt = entity.alt
            };
        }

        public static MultimediaContent ToEntity(MultimediaDTO dto)
        {
            if (dto == null) return null!;
            return new MultimediaContent
            {
                id = dto.Id,
                url = dto.URL,
                alt = dto.Alt
            };
        }


    }
}
