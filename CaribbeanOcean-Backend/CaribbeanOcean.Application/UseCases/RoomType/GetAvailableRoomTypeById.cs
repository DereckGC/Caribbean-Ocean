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
    public class GetAvailableRoomTypeById(IRoomTypeRepository roomTypeRepository)
    {

        public async Task<RoomTypeDTO> ExecuteAsync(RoomTypeRequestDTO dto)
        {
            if(dto.RoomTypeId == null) {
                throw new ValidationException("RoomTypeId is required.");
            }
            var roomType = await roomTypeRepository.GetAvailableRoomTypeById(dto.RoomTypeId, dto.CheckIn, dto.CheckOut);
            
            if (roomType == null) {
                throw new ConflictException("No available room found for the specified type and dates.");
            }

            return RoomTypeMapper.ToDto(roomType);


        }
    }
}
