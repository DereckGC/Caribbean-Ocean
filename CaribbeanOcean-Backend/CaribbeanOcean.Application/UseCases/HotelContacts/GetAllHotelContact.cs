using CaribbeanOcean.Application.DTOs.ContactUs;
using CaribbeanOcean.Application.DTOs.HotelContact;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.HotelContacts
{
    public class GetAllHotelContact (IHotelContactRepository _repository)
    {
        public async Task<IEnumerable<HotelContactDTO>> ExecuteAsync()
        {
            var hotelContacts = await _repository.GetAll();

            return hotelContacts.Select(contact => HotelContactMapper.ToDTO(contact));
        }
    }
}
