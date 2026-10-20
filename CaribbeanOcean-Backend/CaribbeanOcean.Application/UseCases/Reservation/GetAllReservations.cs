using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs;

using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetAllReservations(IReservationRepository reservationRepository)
    {
        public async Task<IEnumerable<ReservationDTO>> ExecuteAsync()
        {
            var entities = (await reservationRepository.getAllReservations()).ToList();
            var dtos = new List<ReservationDTO>();

            foreach(var entity in entities)
            {
                var details = await reservationRepository.GetReservationDetailsByReservationId(entity.Id);
                var dto = ReservationMapper.ToDTO(entity, details);
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
