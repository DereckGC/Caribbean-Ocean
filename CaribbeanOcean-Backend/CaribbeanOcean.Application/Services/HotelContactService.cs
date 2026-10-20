using CaribbeanOcean.Application.DTOs.ContactUs;
using CaribbeanOcean.Application.UseCases.HotelContacts;
using CaribbeanOcean.Application.DTOs.HotelContact;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Services
{
    public class HotelContactService(GetAllHotelContact getAllHotelContacts,
        CreateHotelContact createHotelContact,
        GetHotelContactById getHotelContactById,
        UpdateHotelContact updateHotelContact,
        DeleteHotelContact deleteHotelContact
)
    {
        public async Task<IEnumerable<HotelContactDTO>> GetAll() => await getAllHotelContacts.ExecuteAsync();

        public async Task<HotelContactDTO?> GetById(int id) => await getHotelContactById.ExecuteAsync(id);

        public async Task<int> Create(HotelContactDTO dto) => await createHotelContact.ExecuteAsync(dto);

        public async Task Update(int id, HotelContactDTO dto)
        {
            var contactDto = new HotelContactDTO
            {
                Id = id,
                Type = dto.Type,
                Contact = dto.Contact
            };
            await updateHotelContact.ExecuteAsync(contactDto);
        }

        public async Task Delete(int id) => await deleteHotelContact.ExecuteAsync(id);

    }
}













