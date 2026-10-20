using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetAllAvailableRoomTypes(IRoomTypeRepository roomTypeRepository)
    {


        public async Task<IEnumerable<RoomTypeDTO>> ExecuteAsync(RoomTypeRequestDTO dto)
        {
            var roomTypes = await roomTypeRepository.GetAllAvailableRoomTypes(dto.CheckIn, dto.CheckOut);
            var roomTypeDTOs = new List<RoomTypeDTO>();

            foreach (var roomType in roomTypes)
            {
                roomTypeDTOs.Add(RoomTypeMapper.ToDto(roomType));
            }

            return roomTypeDTOs;
        }
    }
}
