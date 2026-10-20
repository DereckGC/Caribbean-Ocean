using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class DeleteReservation(IReservationRepository reservationRepository, IReservationNightDetailRepository reservationNightDetailRepository)
    {
        public async Task<bool> ExecuteAsync(int id)
        {
            var sucess = await reservationRepository.deleteReservation(id);
           if(sucess)
           {
                await reservationNightDetailRepository.DeactivateByReservationId(id);
                return true;
            }
           return false;
        }
    }
}
