using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class DeleteRoomType(IRoomTypeRepository roomTypeRepository)
    {
        public async Task ExecuteAsync(int id)
        {
            var existing = await roomTypeRepository.getRoomTypeById(id);

            if (existing == null)
                throw new NotFoundException($"Room type with id {id} was not found.");

            await roomTypeRepository.deleteRoomType(id);
        }
    }
}