using System.Collections.Generic;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Application.UseCases;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.Services
{
    // Make sure you inject any future Use Cases (like Update/Delete) here as well!
    public class RoomService(
        CreateRoom createRoom,
        GetRoomById getRoomById,
        GetAllRooms getAllRooms,
        UpdateRoom updateRoom,
        DeleteRoom deleteRoom,
        GetRoomStatusToday getRoomStatusToday)
    {
        public async Task<int> Create(CreateRoomDTO dto) => await createRoom.ExecuteAsync(dto);
        public async Task<RoomDTO?> GetById(int id) => await getRoomById.ExecuteAsync(id);
        public async Task<IEnumerable<RoomDTO>> GetAll() => await getAllRooms.ExecuteAsync();
        public async Task<bool> Update(UpdateRoomDTO dto) => await updateRoom.ExecuteAsync(dto);

        public async Task<bool> Delete(int id) => await deleteRoom.ExecuteAsync(id);

        public async Task<List<AvailableRoomInfo>> GetRoomStatusToday(int? roomTypeId)
            => await getRoomStatusToday.Execute(roomTypeId);
    }
}
