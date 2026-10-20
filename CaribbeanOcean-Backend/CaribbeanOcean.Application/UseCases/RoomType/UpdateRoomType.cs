using System.Text.Json;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class UpdateRoomType(IRoomTypeRepository roomTypeRepository)
    {
        public async Task ExecuteAsync(int id, UpdateRoomTypeDTO dto)
        {
            var existing = await roomTypeRepository.getRoomTypeById(id);

            if (existing == null)
                throw new NotFoundException($"Room type with id {id} was not found.");

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.DailyRate = dto.DailyRate;
            existing.ImageUrl = dto.ImageUrl ?? existing.ImageUrl;
            existing.features = dto.Features != null     
                ? JsonSerializer.Serialize(dto.Features)
                : existing.features;

            await roomTypeRepository.updateRoomType(existing);
        }
    }
}