using System.Collections.Generic;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Application.UseCases;

using CaribbeanOcean.Application.UseCases;
using CaribbeanOcean.Application.UseCases.RoomType;

namespace CaribbeanOcean.Application.Services
{
    public class RoomTypeService(
        CreateRoomType createRoomType,
        GetAllAvailableRoomTypes getAllAvailableRoomTypes,
        GetAvailableRoomTypeById getAvailableRoomTypeById,
        GetAllAvailableRoomTypesCount getAllAvailableRoomTypesCount,
        GetClientPageRoomType getClientPageRoomType,
        GetAllRoomTypesUseCase getAllRoomTypes,
        UpdateRoomType updateRoomType,
        DeleteRoomType deleteRoomType
        )
    {
        // Notice Create returns the integer ID
        public async Task<int> Create(CreateRoomTypeDTO dto) => await createRoomType.ExecuteAsync(dto);
        public async Task Update(int id, UpdateRoomTypeDTO dto) => await updateRoomType.ExecuteAsync(id, dto);
        public async Task Delete(int id) => await deleteRoomType.ExecuteAsync(id);
        //public async Task<RoomTypeDTO?> GetById(int id) => await getRoomTypeById.ExecuteAsync(id);
        public async Task<RoomTypePageDTO> GetRoomTypPage() => await getClientPageRoomType.ExecuteAsync();
        public async Task<IEnumerable<RoomTypeDTO>> GetAll() => await getAllRoomTypes.ExecuteAsync();
        public async Task<IEnumerable<RoomTypeDTO>> GetAllAvailable(RoomTypeRequestDTO dto) => await getAllAvailableRoomTypes.ExecuteAsync(dto);
         public async Task<RoomTypeDTO?> GetAvailableById(RoomTypeRequestDTO dto) => await getAvailableRoomTypeById.ExecuteAsync(dto);
         public async Task<IEnumerable<AvailableRoomTypeInfoDTO>> GetAllAvailableCount(RoomTypeRequestDTO dto) => await getAllAvailableRoomTypesCount.ExecuteAsync(dto);

    }
}
