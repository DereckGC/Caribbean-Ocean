using System.Collections.Generic;
using System.Threading.Tasks;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room?> getRoomById(int id);
        Task<IEnumerable<Room>> getAllRooms();


        Task<int> createRoom(Room room);

        Task<bool> updateRoom(Room room);
        Task<bool> deleteRoom(int id);

        Task<bool> HasAvailableRoomByType(int? roomTypeId, DateTime checkIn, DateTime checkOut);


        Task<bool> HasAvailableRoomByTypeForUpdate(int? roomTypeId, DateTime checkIn, DateTime checkOut, int reservationId);
        Task<int> GetAvailableRoomIdByType(int roomTypeId, DateTime checkIn, DateTime checkOut);

        Task<List<AvailableRoomInfo>> GetRoomStatusToday(int? roomTypeId);

        Task<int> GetAvailableRoomIdByTypeForUpdate(
                int reservationId,
                int roomTypeId,
                DateTime checkIn,
                DateTime checkOut);

    }
}
