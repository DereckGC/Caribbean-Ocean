using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetRoomTypeByIdUseCase(IRoomTypeRepository roomTypeRepository)
    {
        public async Task<RoomTypeDTO?> ExecuteAsync(int id)
        {
            // Talk to the database
            var roomTypeEntity = await roomTypeRepository.getRoomTypeById(id);
            return RoomTypeMapper.ToDto(roomTypeEntity);
        }
    }
}
