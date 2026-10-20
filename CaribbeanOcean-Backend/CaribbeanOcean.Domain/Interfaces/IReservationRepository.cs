using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> getReservationById(int id);
        Task<IEnumerable<Reservation>> getAllReservations();
        Task<int> createReservation(Reservation reservation);
        Task<bool> updateReservation(Reservation reservation);
        Task<bool> deleteReservation(int id);

        Task<ReservationDetails?> GetReservationDetailsByReservationId(int reservationId);
    }
}
