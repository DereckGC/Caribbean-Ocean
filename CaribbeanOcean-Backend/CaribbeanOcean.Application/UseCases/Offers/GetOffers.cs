using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Offers
{
    public class GetOffers(IOfferRepository _repository)
    {
        public async Task<IEnumerable<OfferDTO>> ExecuteAsync()
        {
            var offers = await _repository.GetClientOffers();

            return offers.Select(offer => OfferMapper.ToDTO(offer));
        }
    }
}
