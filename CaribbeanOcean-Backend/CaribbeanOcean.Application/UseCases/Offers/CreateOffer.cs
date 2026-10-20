using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Offers
{
    public class CreateOffer(IOfferRepository _repository)
    {
        public async Task<bool> ExecuteAsync(OfferAdminDTO offer)
        {
            if (offer == null) {
                throw new ValidationException("Offer cannot be null.");
            }
            await _repository.ValidateOfferRegistration(OfferMapper.ToEntity(offer));
            ValidateOffer.Validate(offer);
            int newId = await _repository.Create(OfferMapper.ToEntity(offer));
            return newId > 0;
        }
    }
}
