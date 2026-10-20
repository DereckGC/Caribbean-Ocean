using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetRoomById(IRoomRepository roomRepository)
    {
        public async Task<RoomDTO?> ExecuteAsync(int id)
        {
            var entity = await roomRepository.getRoomById(id);
            if (entity == null) return null;
            return RoomMapper.ToDto(entity);
        }
    }
}
