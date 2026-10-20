using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CaribbeanOcean.Application.UseCases.Offers
{
    public class UpdateOffer(IOfferRepository _repository)
    {
        public async Task<bool> ExecuteAsync(OfferAdminDTO offerDTO)
        {
            if (offerDTO == null) throw new ValidationException("Offer cannot be null.");

            ValidateOffer.Validate(offerDTO);

            var entity = OfferMapper.ToEntity(offerDTO);
            Offer originOffer = await _repository.GetById(entity.id);

            if (entity.start_time != originOffer.start_time ||
                entity.end_time != originOffer.end_time ||
                entity.room_type_id != originOffer.room_type_id)
            {
                await _repository.ValidateOfferRegistration(entity);
            }

            return await _repository.Update(entity);
        }

    }
}
