using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Application.DTOs.ContactUs;
using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Application.DTOs.HotelContact;
using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Mappers
{
    public class HotelContactMapper
    {
        public static HotelContactDTO ToDTO(HotelContact entity)
        {
            if (entity == null) return null!;
            return new HotelContactDTO
            {   Id= entity.id,
                Type = entity.type,
                Contact = entity.contact
            };
        }

        public static HotelContact ToEntity(HotelContactDTO dto)
        {
            if (dto == null) return null!;
            return new HotelContact
            {
                id=dto.Id,
                type = dto.Type,
                contact = dto.Contact
            };
        }
    }
}
