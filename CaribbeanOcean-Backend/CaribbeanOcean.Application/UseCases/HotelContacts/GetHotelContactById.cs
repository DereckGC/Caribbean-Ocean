using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.HotelContacts
{
   public class GetHotelContactById (IHotelContactRepository hotelContactRepository)
    {
        public async Task<DTOs.HotelContact.HotelContactDTO?> ExecuteAsync(int id)
        {
            var entity = await hotelContactRepository.GetById(id);
            return entity is null ? null : Mappers.HotelContactMapper.ToDTO(entity);
        }
    }
}
