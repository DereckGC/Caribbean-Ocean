using CaribbeanOcean.Application.DTOs.HotelContact;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.HotelContacts
{
    public class UpdateHotelContact(IHotelContactRepository hotelContactRepository)
    {
        public async Task <bool> ExecuteAsync(HotelContactDTO dto)
        {
            var existingEntity = await hotelContactRepository.GetById(dto.Id);
            if (existingEntity == null) 
            {
                throw new NotFoundException("Hotel contact not found.");
            }
            else
            {
               existingEntity.type = dto.Type;
               existingEntity.contact = dto.Contact;
               existingEntity.id = dto.Id;
              return await hotelContactRepository.Update(existingEntity);
               
            }

        }
    }
}
