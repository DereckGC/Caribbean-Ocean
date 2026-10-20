using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class CheckRoomReservationAvailability(IReservationRepository reservationRepository, IRoomRepository roomRepository)
    {
        public async Task<bool> ExecuteAsync(RoomTypeRequestDTO dto)
        {
            if (dto.CheckOut <= dto.CheckIn)
            {
                throw new ValidationException("Check-out date must be after check-in.");
            }


            bool hasAvailableRoom = await roomRepository.HasAvailableRoomByType(dto.RoomTypeId, dto.CheckIn, dto.CheckOut);
            return hasAvailableRoom;
        }
    }
}
