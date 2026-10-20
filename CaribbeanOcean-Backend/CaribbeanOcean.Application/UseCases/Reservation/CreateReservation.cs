using System;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Pricing;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Application.Services;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
namespace CaribbeanOcean.Application.UseCases

{
    public class CreateReservation(IReservationRepository reservationRepository, IRoomRepository roomRepository, IReservationNightDetailRepository reservationNightDetailRepository,ReservationPricingService reservationPricingService)
    {
        public async Task<string> ExecuteAsync(CreateReservationDTO dto)
        {
           
            if (dto.CheckOut <= dto.CheckIn)
            {
                throw new ValidationException("Check-out date must be after check-in.");
            }

            if (dto.ApplyOffers && dto.SelectedOfferId == null)
            {
                throw new ValidationException("Selected offer is required when applying offers.");
            }

            //AQUI SE OBTIENE EL PRECIO DE LA RESERVA, SI SE APLICAN OFERTAS O NO, PARA LUEGO GUARDARLO EN LA RESERVA Y EN LOS DETALLES DE NOCHE
            var priceResult = await reservationPricingService.Calculate(
               new CalculateReservationPriceRequestDTO
               {
                   RoomTypeId = dto.RoomTypeId,
                   CheckIn = dto.CheckIn,
                   CheckOut = dto.CheckOut,
                   ApplyOffers = dto.ApplyOffers,
                   SelectedOfferId = dto.SelectedOfferId
               }
           );



            int foundAvailableRoomId = await roomRepository.GetAvailableRoomIdByType(dto.RoomTypeId, dto.CheckIn, dto.CheckOut);
            if (foundAvailableRoomId <= 0)
            {
                throw new ConflictException("No available room found for the specified type and dates.");
            }

            Console.WriteLine($"Found available Room ID: {foundAvailableRoomId} for Room Type ID: {dto.RoomTypeId}");
            // 2. Generate a unique reservation code
            string generatedCode = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 14);

            // 3. Map the DTO to our Domain Entity

           var newReservation = ReservationMapper.ToEntityCreate(dto, generatedCode, foundAvailableRoomId, priceResult.TotalAmount);

            // 4. Send the Entity to the Infrastructure layer to be saved
            var reservationId = await reservationRepository.createReservation(newReservation);

            // After creating the reservation, we need to save the night details for each night of the stay
            var nightDetails = priceResult.Nights.Select(night => new ReservationNightDetail
            {
                ReservationId = reservationId,
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

            await reservationNightDetailRepository.AddRange(
                reservationId,
                nightDetails
            );
            // 5. Return the generated code to the frontend
            return generatedCode;
        }
    }
}
