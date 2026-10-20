using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Offers
{
    public class GetAdminOffers(IOfferRepository _repository)
    {
        public async Task<IEnumerable<OfferAdminDTO>> ExecuteAsync()
        {
            var offers = await _repository.GetAll();

            return offers.Select(offer => OfferMapper.ToAdminDTO(offer));
        }
    }
}
