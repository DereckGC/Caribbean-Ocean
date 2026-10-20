using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetAllAvailableRoomTypesCount(IRoomTypeRepository roomTypeRepository) 
    {


        public async Task<IEnumerable<AvailableRoomTypeInfoDTO>> ExecuteAsync(RoomTypeRequestDTO dto)
        {
            if (dto.CheckIn >= dto.CheckOut)
            {
                throw new ValidationException("Check-out date must be after check-in date.");
            }
            var roomTypes = await roomTypeRepository.GetAllAvailableRoomTypesWithCount(dto.CheckIn, dto.CheckOut);
            
            var roomTypeDTOs = new List<AvailableRoomTypeInfoDTO>();

            foreach (var roomType in roomTypes)
            {
                roomTypeDTOs.Add(RoomTypeMapper.ToDTOInfo(roomType));
            }

            return roomTypeDTOs;
        }
    }
}
