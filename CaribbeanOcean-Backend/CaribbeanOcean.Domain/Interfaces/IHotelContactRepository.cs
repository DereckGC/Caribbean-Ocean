using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IHotelContactRepository
    {
        Task<IEnumerable<HotelContact>> GetAll();
        Task<HotelContact> GetById(int id);
        Task<int> Create(HotelContact hotelContact);
        Task<bool> Update(HotelContact hotelContact);
        Task<bool> Delete(int id);
    }
}
