using System.Collections.Generic;
using System.Linq;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Application.DTOs.Room;
namespace CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;

public static class RoomMapper
    {
        public static RoomDTO ToDto(Room entity)
        {
            if (entity == null) return null!;

            return new RoomDTO
            {
                Id = entity.Id,
                Number = entity.Number,
                RoomTypeId = entity.RoomTypeId,
                Active = entity.Active
            };
        }

        public static IEnumerable<RoomDTO> ToDtoList(IEnumerable<Room> entities)
        {
            return entities.Select(ToDto);
        }

        public static Room ToEntity(CreateRoomDTO dto)
        {
            if (dto == null) return null!;
            return new Room
            {
                Number = dto.Number,
                RoomTypeId = dto.RoomTypeId,
                Active = dto.Active
            };
        }

        public static Room ToEntity(UpdateRoomDTO dto)
        {
            if (dto == null) return null!;
            return new Room
            {
                Id = dto.Id,
                Number = dto.Number,
                RoomTypeId = dto.RoomTypeId,
                Active = dto.Active
            };
        }


        public static AvailableRoomDTO ToDTO(AvailableRoomInfo room)
        {
            if (room == null) return null!;

            return new AvailableRoomDTO
            {
                Number = room.Number,
                RoomType = room.RoomType,
                Status = room.Status
            };
        }

        public static IEnumerable<AvailableRoomDTO> ToAvailableRoomDTOList(
            IEnumerable<AvailableRoomInfo> rooms)
        {
            return rooms.Select(ToDTO);
        }
    }

