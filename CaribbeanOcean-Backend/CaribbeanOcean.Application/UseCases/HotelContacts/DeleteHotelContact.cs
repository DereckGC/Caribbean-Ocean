using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.HotelContacts
{
    public class DeleteHotelContact(IHotelContactRepository hotelContactRepository)
    {
        public async Task ExecuteAsync(int id)
        {
            await hotelContactRepository.Delete(id);
        }
    }
}
