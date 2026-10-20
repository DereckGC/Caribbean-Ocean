using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using RoomEntity = CaribbeanOcean.Domain.Entities.Room;
namespace CaribbeanOcean.Application.UseCases
{
    public class CreateRoom(IRoomRepository roomRepository)
    {
        public async Task<int> ExecuteAsync(CreateRoomDTO dto)
        {
            return await roomRepository.createRoom(RoomMapper.ToEntity(dto));
        }
    }
}
