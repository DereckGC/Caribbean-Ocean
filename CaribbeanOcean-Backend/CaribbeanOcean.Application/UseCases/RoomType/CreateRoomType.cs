using System;
using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    
    public class CreateRoomType(IRoomTypeRepository roomTypeRepository)
    {
        public async Task<int> ExecuteAsync(CreateRoomTypeDTO dto)
        {
            
            return await roomTypeRepository.createRoomType(RoomTypeMapper.ToEntity(dto));
        }


    }
}
