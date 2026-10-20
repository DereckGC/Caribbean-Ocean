using CaribbeanOcean.Application.DTOs.Pricing;
using CaribbeanOcean.Application;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class CalculatePrice
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IOfferRepository _offerRepository;

        public CalculatePrice(
            IRoomTypeRepository roomTypeRepository,
            ISeasonRepository seasonRepository,
            IOfferRepository offerRepository)
        {
            _roomTypeRepository = roomTypeRepository;
            _seasonRepository = seasonRepository;
            _offerRepository = offerRepository;
        }

        public async Task<ReservationPriceResultDTO> ExecuteAsync(
            CalculateReservationPriceRequestDTO request)
        {
            if (request.CheckIn.Date >= request.CheckOut.Date)
            {
                throw new ValidationException("Check-out date must be after check-in date.");
            }

            if (request.ApplyOffers && request.SelectedOfferId == null)
            {
                throw new ValidationException("Selected offer is required when applying offers.");
            }

            var roomType = await _roomTypeRepository.getRoomTypeById(request.RoomTypeId);

            if (roomType == null)
            {
                throw new ArgumentException("Room type not found.");
            }

            var result = new ReservationPriceResultDTO();

            for (var date = request.CheckIn.Date;date < request.CheckOut.Date;date = date.AddDays(1)
            )
            {
                var basePrice = roomType.DailyRate;

                var season = await _seasonRepository.GetActiveSeasonByDate(date);

                var seasonAdjustment = season?.discountAmount ?? 0;

                var priceWithSeason = basePrice * (1 + seasonAdjustment / 100m);

                Offer? offer = null;

                if (request.ApplyOffers && request.SelectedOfferId.HasValue)
                {
                    offer = await _offerRepository.GetApplicableOfferById(request.SelectedOfferId.Value,request.RoomTypeId,date
                    );
                }


                var offerDiscount = offer?.discount_percentage ?? 0;

                var finalNightPrice = priceWithSeason * (1 - offerDiscount / 100m);

                finalNightPrice = Math.Round(finalNightPrice, 2);

                result.Nights.Add(new ReservationNightPriceDetailResultDTO
                {
                    StayDate = date,
                    BasePrice = basePrice,

                    SeasonId = season?.id,
                    SeasonName = season?.name,
                    SeasonAdjustmentPercentage = seasonAdjustment,

                    OfferId = offer?.id,
                    OfferName = offer?.name,
                    OfferDiscountPercentage = offerDiscount,

                    FinalNightPrice = finalNightPrice,
                });

                result.TotalAmount += finalNightPrice;
            }

            result.TotalAmount = Math.Round(result.TotalAmount, 2);
            result.NightCount = result.Nights.Count;

            result.AverageNightlyRate = result.NightCount > 0
                ? Math.Round(result.TotalAmount / result.NightCount, 2)
                : 0;

            return result;
        }
    }
}