using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs;

using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetReservationById(IReservationRepository reservationRepository)
    {
        public async Task<ReservationDTO?> ExecuteAsync(int id)
        {
            var entity = await reservationRepository.getReservationById(id);
            if (entity == null) {
                throw new NotFoundException($"Reservation with id {id} was not found.");
            }

            var details = await reservationRepository.GetReservationDetailsByReservationId(id);
            if (details == null) { 
                throw new NotFoundException($"Details for reservation with id {id} were not found.");
            }
            
            return ReservationMapper.ToDTO(entity, details);
        }
    }
}
