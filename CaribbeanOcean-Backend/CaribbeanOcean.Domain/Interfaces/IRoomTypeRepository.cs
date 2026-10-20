using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<RoomType?> getRoomTypeById(int id);
        Task<IEnumerable<RoomType>> getAllRoomTypes();
        Task<int> createRoomType(RoomType roomType);

        Task<bool> updateRoomType(RoomType roomType);

        Task<bool> deleteRoomType(int id);
        Task<RoomType?> GetAvailableRoomTypeById(int? roomTypeId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<RoomType>> GetAllAvailableRoomTypes(DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<AvailableRoomTypeInfo>> GetAllAvailableRoomTypesWithCount(DateTime checkIn, DateTime checkOut);
    }
}
