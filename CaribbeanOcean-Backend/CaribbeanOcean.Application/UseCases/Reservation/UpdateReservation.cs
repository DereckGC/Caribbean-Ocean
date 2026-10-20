using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Pricing;
using CaribbeanOcean.Application.Services;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class UpdateReservation(IReservationRepository reservationRepository, IRoomRepository roomRepository,IReservationNightDetailRepository reservationNightDetailRepository, ReservationPricingService reservationPricingService)
    {
        public async Task<bool> ExecuteAsync(ReservationDTO dto)
        {
            if (dto.CheckOut <= dto.CheckIn)
            {
                throw new ValidationException("Check-out date must be after check-in.");
            }

            var existingEntity = await reservationRepository.getReservationById(dto.Id);

            if (existingEntity == null) 
            {
                throw new NotFoundException("Reservation not found.");
            }

            var reservationDetails = await reservationRepository.GetReservationDetailsByReservationId(dto.Id);

            var datesChanged =
                existingEntity.CheckIn.Date != dto.CheckIn.Date ||
                existingEntity.CheckOut.Date != dto.CheckOut.Date;

                existingEntity.ClientName = dto.ClientName;
                existingEntity.ClientLastname = dto.ClientLastname;
                existingEntity.Email = dto.Email;

            if (!datesChanged)
            {
                // Keep original dates and total amount.
                // Do not touch night details.
                return await reservationRepository.updateReservation(existingEntity);
            }



            if (existingEntity.CheckIn != dto.CheckIn || existingEntity.CheckOut != dto.CheckOut)
            {
                int foundAvailableRoomId = await roomRepository.GetAvailableRoomIdByTypeForUpdate(existingEntity.Id, reservationDetails.RoomTypeId, dto.CheckIn, dto.CheckOut);
                if (foundAvailableRoomId <= 0)
                {
                    throw new ConflictException("No available room found for the specified type and dates.");
                }
                existingEntity.RoomId = foundAvailableRoomId; // Update the RoomId to the newly found available room
            }
            /// Calculate de price Result
            /// 
                var priceResult = await reservationPricingService.Calculate(
                new CalculateReservationPriceRequestDTO
                {
                    RoomTypeId = existingEntity.RoomId,
                    CheckIn = dto.CheckIn,
                    CheckOut = dto.CheckOut,

                // Keep the same offer behavior used when the reservation was created.
                ApplyOffers = existingEntity.PricingUsedOffer,
                SelectedOfferId = existingEntity.SelectedOfferId
                }
        );
            
           
            existingEntity.CheckIn = dto.CheckIn;
            existingEntity.CheckOut = dto.CheckOut;
            existingEntity.TotalAmount = priceResult.TotalAmount;

            // Note: We deliberately DO NOT touch existingEntity.Code, existingEntity.CreatedAt, or TransactionId!

            // 3. Save the safely updated entity
            var updated = await reservationRepository.updateReservation(existingEntity);

            if (!updated)
            {
                return false;
            }

            await reservationNightDetailRepository.DeactivateByReservationId(existingEntity.Id);

            var nightDetails = priceResult.Nights.Select(night => new ReservationNightDetail
            {
                ReservationId = existingEntity.Id,
                StayDate = night.StayDate,
                BasePrice = night.BasePrice,
                SeasonId = night.SeasonId,
                SeasonName = night.SeasonName,
                SeasonAdjustmentPercentage = night.SeasonAdjustmentPercentage,
                OfferId = night.OfferId,
                OfferName = night.OfferName,
                OfferDiscountPercentage = night.OfferDiscountPercentage,
                FinalNightPrice = night.FinalNightPrice,
                Active = true,
                CreatedAt = DateTime.Now
            }).ToList();

            await reservationNightDetailRepository.AddRange(existingEntity.Id,nightDetails);


            return true ;
        }
    }
}
