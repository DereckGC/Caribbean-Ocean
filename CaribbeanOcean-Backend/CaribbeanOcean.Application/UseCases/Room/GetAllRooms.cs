using System.Collections.Generic;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetAllRooms(IRoomRepository roomRepository)
    {
        public async Task<IEnumerable<RoomDTO>> ExecuteAsync()
        {
            var entities = await roomRepository.getAllRooms();
            return RoomMapper.ToDtoList(entities);
        }
    }
}
