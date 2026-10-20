using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Offers
{
    public class DeleteOffer(IOfferRepository _repository)
    {
        public async Task<bool> ExecuteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Offer ID must be positive.");
            }

            return await _repository.Delete(id);
        }
    }
}
