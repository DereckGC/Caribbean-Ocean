using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases
{
    public class DeleteRoom(IRoomRepository roomRepository)
    {
        public async Task<bool> ExecuteAsync(int id)
        {
            var existing = await roomRepository.getRoomById(id)
                ?? throw new NotFoundException($"Room with id {id} not found.");

            return await roomRepository.deleteRoom(id);
        }
    }
}
