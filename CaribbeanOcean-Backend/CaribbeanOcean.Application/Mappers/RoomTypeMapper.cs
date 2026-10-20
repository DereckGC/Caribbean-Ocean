using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.Mappers
{
    public static class RoomTypeMapper
    {
        public static RoomTypeDTO ToDto(RoomType entity)
        {
            if (entity == null) return null!;

            return new RoomTypeDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DailyRate = entity.DailyRate,
                ImageUrl = entity.ImageUrl,
                Features = string.IsNullOrWhiteSpace(entity.features)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(entity.features) ?? new List<string>(),
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static RoomType ToEntity(RoomTypeDTO dto)
        {
            if (dto == null) return null!;
            return new RoomType
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                DailyRate = dto.DailyRate,
                ImageUrl = dto.ImageUrl,
                features = dto.Features != null
                    ? JsonSerializer.Serialize(dto.Features)
                    : null,
                UpdatedAt = dto.UpdatedAt
            };
        }

        public static RoomType ToEntity(CreateRoomTypeDTO dto)
        {
            if (dto == null) return null!;
            return new RoomType
            {
                Name = dto.Name,
                Description = dto.Description,
                DailyRate = dto.DailyRate,
                ImageUrl = dto.ImageUrl,
                features = dto.Features != null     // ← AGREGAR
                    ? JsonSerializer.Serialize(dto.Features)
                    : null
            };
        }

        public static RoomTypePageDTO ToPageDTO(PageContent pageContent, IEnumerable<RoomType> room_type)
        {
            if (pageContent == null || room_type == null) return null!;
            return new RoomTypePageDTO
            {
                Title = pageContent.title,
                Subtitle = pageContent.subtitle,
                rooms = ToDtoList(room_type)
            };
        }

        public static AvailableRoomTypeInfoDTO ToDTOInfo(AvailableRoomTypeInfo entity)
        {
            if (entity == null) return null!;
            return new AvailableRoomTypeInfoDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DailyRate = entity.DailyRate,
                ImageUrl = entity.ImageUrl,
                UpdatedAt = entity.UpdatedAt,
                Active = entity.Active,
                AvailableRooms = entity.AvailableRooms
            };
        }

        public static IEnumerable<RoomTypeDTO> ToDtoList(IEnumerable<RoomType> entities)
        {
            return entities.Select(ToDto);
        }
    }
}