using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IReservationNightDetailRepository
    {
        Task AddRange(int reservationId,List<ReservationNightDetail> nights
        );

        Task DeactivateByReservationId(int reservationId);

        Task<List<ReservationNightDetail>> GetActiveByReservationId(int reservationId);
    }
}
