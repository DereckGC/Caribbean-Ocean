using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetAll();
        Task<IEnumerable<Offer>> GetClientOffers();
        Task<Offer> GetById(int id);
        Task<int> Create(Offer offer);
        Task<bool> Update(Offer offer);
        Task<bool> Delete(int id);
        Task<Offer?> GetApplicableOfferById(int offerId, int roomTypeId, DateTime date);
        Task<bool> ValidateOfferRegistration(Offer offer);
    }
}
