using System.Collections.Generic;
using System.Linq;
using CaribbeanOcean.Application.DTOs;

using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.Mappers
{
    public static class ReservationMapper
    {
        public static ReservationDTO ToDTO(Reservation entity, ReservationDetails details)
        {
            if (entity == null) return null!;

            return new ReservationDTO
            {
                Id = entity.Id,
                Code = entity.Code,
                RoomId = entity.RoomId,
               Email = entity.Email,
                ClientName = entity.ClientName,
                ClientLastname = entity.ClientLastname,
                CreditCardMasked = entity.MaskedCard,
                CheckIn = entity.CheckIn,
                CheckOut = entity.CheckOut,
                TotalAmount = entity.TotalAmount,
                RoomTypeId = details.RoomTypeId,
                RoomTypeName = details.RoomTypeName,
               

            };
        }
        
        public static IEnumerable<ReservationDTO> ToDTOList(IEnumerable<Reservation> entities, IEnumerable<ReservationDetails> detailsList)
        {
            return entities.Zip(detailsList, (entity, details) => ToDTO(entity, details));
        }

        public static Reservation ToEntityCreate(CreateReservationDTO dto, string generatedCode, int foundAvailableRoomId, decimal totalAmount)
        {
            if (dto == null) return null!;
            return new Reservation
            {
                
                Code = generatedCode,
                RoomId = foundAvailableRoomId,
                Email = dto.Email,
                ClientName = dto.ClientName,
                ClientLastname = dto.ClientLastname,

                // Mask the credit card, keeping only the last 4 digits
                MaskedCard = dto.CardNumber.Length >= 4
                             ? "****" + dto.CardNumber.Substring(dto.CardNumber.Length - 4)
                             : "****",

                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                PricingUsedOffer = dto.ApplyOffers,
                SelectedOfferId = dto.ApplyOffers
                    ? dto.SelectedOfferId
                    : null,

                // Set the exact time the reservation was made on the server side
                CreatedAt = DateTime.UtcNow,
                TotalAmount = totalAmount

            };
        }
        
        public static Reservation ToEntity(CreateReservationDTO dto)
        {
            if (dto == null) return null!;
            return new Reservation
            {
                
                RoomId = dto.RoomTypeId,
                
                ClientName = dto.ClientName,
                ClientLastname = dto.ClientLastname,
                Email = dto.Email,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                
            };
        }
    }
}
