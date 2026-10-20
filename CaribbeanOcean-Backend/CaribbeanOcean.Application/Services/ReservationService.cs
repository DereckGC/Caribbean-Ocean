using System.Collections.Generic;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Reservation;
using CaribbeanOcean.Application.UseCases;
using CaribbeanOcean.Application.UseCases.Reservation;



namespace CaribbeanOcean.Application.Services
{
    // The Service acts as a Facade, injecting the 5 separate Use Cases
    public class ReservationService(
        CreateReservation createReservation,
        UpdateReservation updateReservation,
        GetAllReservations getAllReservations,
        GetReservationById getReservationById,
        CheckRoomReservationAvailability checkRoomAvailability,
        DeleteReservation deleteReservation,
        CheckRoomReservationAvailabilityForUpdate checkRoomAvailabilityForUpdate
        ) 
    {
        public async Task<string> Create(CreateReservationDTO dto) => await createReservation.ExecuteAsync(dto);
        public async Task<bool> Update(ReservationDTO dto) => await updateReservation.ExecuteAsync(dto);

        public async Task<bool> Delete(int id) => await deleteReservation.ExecuteAsync(id);
        public async Task<ReservationDTO?> GetById(int id) => await getReservationById.ExecuteAsync(id);
        public async Task<IEnumerable<ReservationDTO>> GetAll() => await getAllReservations.ExecuteAsync();

        public async Task<bool> HasAvailableRoom(RoomTypeRequestDTO dto) => await checkRoomAvailability.ExecuteAsync(dto);

        public async Task<bool> HasAvailableRoomForUpdate(UpdateAvailabilityRequestDTO dto) => await checkRoomAvailabilityForUpdate.ExecuteAsync(dto);

    }
}
