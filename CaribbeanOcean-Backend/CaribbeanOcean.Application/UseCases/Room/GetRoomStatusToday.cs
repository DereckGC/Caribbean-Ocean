using System.Collections.Generic;
using System.Threading.Tasks;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class GetRoomStatusToday
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomStatusToday(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<AvailableRoomInfo>> Execute(int? roomTypeId)
        {
            return await _roomRepository.GetRoomStatusToday(roomTypeId);
        }
    }
}