using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Application.UseCases.Offers;
using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Services
{
    public class OfferService(GetOffers get, GetAdminOffers getAdminOffers, CreateOffer create, UpdateOffer update, DeleteOffer delete)
    {
        public async Task<IEnumerable<OfferDTO>> GetAll() => await get.ExecuteAsync();

        public async Task<bool> Update(OfferAdminDTO offer) => await update.ExecuteAsync(offer);

        public async Task<bool> Delete(int id) => await delete.ExecuteAsync(id);

        public async Task<bool> Create(OfferAdminDTO offer) => await create.ExecuteAsync(offer);

        public async Task<IEnumerable<OfferAdminDTO>> GetAdminOffers() => await getAdminOffers.ExecuteAsync();
    }
}
