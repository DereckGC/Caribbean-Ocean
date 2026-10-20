using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.HotelContact;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.HotelContacts
{
    public class CreateHotelContact (IHotelContactRepository hotelContactRepository)
    {
        public async Task<int> ExecuteAsync(HotelContactDTO dto)
        {
            return await hotelContactRepository.Create(HotelContactMapper.ToEntity(dto));
        }
    }
}
