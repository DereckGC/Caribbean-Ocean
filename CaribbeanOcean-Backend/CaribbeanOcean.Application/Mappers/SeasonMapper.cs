using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Application.DTOs.Season;
namespace CaribbeanOcean.Application.Mappers
{
    public class SeasonMapper
    {
        public static SeasonDTO ToDto(Season entity)
        {
            if (entity == null) return null!;
            return new SeasonDTO
            {
                Id = entity.id,
                Name = entity.name,
                StartDate = entity.startDate,
                EndDate = entity.endDate,
                DiscountAmount = entity.discountAmount
            };
        }

        public static IEnumerable<SeasonDTO> ToDtoList(IEnumerable<Season> entities)
        {
            var dtoList = new List<SeasonDTO>();
            foreach (var entity in entities)
            {
                dtoList.Add(ToDto(entity));
            }
            return dtoList;
        }
        public static Season ToEntity(CreateSeasonDTO dto)
        {
            if (dto == null) return null!;
            return new Season
            {
                name = dto.Name,
                startDate = dto.StartDate,
                endDate = dto.EndDate,
                discountAmount = dto.DiscountAmount
            };
        }

        public static Season ToEntity(UpdateSeasonDTO dto)
        {
            if (dto == null) return null!;
            return new Season
            {
             
                name = dto.Name,
                startDate = dto.StartDate,
                endDate = dto.EndDate,
                discountAmount = dto.DiscountAmount
            };
        }
    }
}
