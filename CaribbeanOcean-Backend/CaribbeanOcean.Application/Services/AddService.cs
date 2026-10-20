using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Add;
using CaribbeanOcean.Application.UseCases.Add;
using Microsoft.AspNetCore.Http;

namespace CaribbeanOcean.Application.Services
{
    public class AddService(CreateAdd create, DeleteAdd delete, GetAdds get, UpdateAdd update)
    {
        public async Task<IEnumerable<AddDTO>> GetAll() => await get.ExecuteAsync();

        public async Task<bool> Create(IFormFile image, string targetURL, int adminId)
            => await create.ExecuteAsync(image, targetURL, adminId);

        public async Task<bool> Update(int id, IFormFile? newImage, UpdateAddRequestDTO request, int adminId)
            => await update.ExecuteAsync(id, newImage, request, adminId);

        public async Task<bool> Delete(int id) => await delete.ExecuteAsync(id);
    }
}