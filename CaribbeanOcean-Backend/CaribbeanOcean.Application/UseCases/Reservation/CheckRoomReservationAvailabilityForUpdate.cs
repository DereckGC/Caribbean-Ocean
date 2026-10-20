using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Reservation;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Reservation
{
    public class CheckRoomReservationAvailabilityForUpdate(IRoomRepository roomRepository)
    {

        public async Task<bool> ExecuteAsync(UpdateAvailabilityRequestDTO dto)
        {
            if (dto.CheckOut <= dto.CheckIn)
            {
                throw new ValidationException("Check-out date must be after check-in.");
            }


            bool hasAvailableRoom = await roomRepository.HasAvailableRoomByTypeForUpdate(dto.RoomTypeId, dto.CheckIn, dto.CheckOut, dto.ReservationId);
            return hasAvailableRoom;
        }
    }

}
