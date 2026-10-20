using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Application.Mappers;

namespace CaribbeanOcean.Application.UseCases
{
    public class UpdateRoom(IRoomRepository roomRepository)
    {
        public async Task<bool> ExecuteAsync(UpdateRoomDTO dto)
        {
            var existing = await roomRepository.getRoomById(dto.Id)
                ?? throw new NotFoundException($"Room with id {dto.Id} not found.");

            existing.Number = dto.Number;
            existing.RoomTypeId = dto.RoomTypeId;
            existing.Active = dto.Active;

            return await roomRepository.updateRoom(existing);
        }
    }
}
