using System.Collections.Generic;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetAllRoomTypesUseCase(IRoomTypeRepository roomTypeRepository)
    {
        public async Task<IEnumerable<RoomTypeDTO>> ExecuteAsync()
        {
            // Talk to the database
            var entities = await roomTypeRepository.getAllRoomTypes();

            // Automatically maps every result in the list!
            return RoomTypeMapper.ToDtoList(entities);
        }


    }
}
