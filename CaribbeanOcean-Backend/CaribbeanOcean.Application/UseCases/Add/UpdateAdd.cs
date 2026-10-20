using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CaribbeanOcean.Application.DTOs.Add;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CaribbeanOcean.Application.UseCases.Add
{
    public class UpdateAdd(IAddRepository _repository, ICloudinaryService cloudinaryService)
    {
        public async Task<bool> ExecuteAsync(int id, IFormFile? newImage, UpdateAddRequestDTO request, int adminId)
        {
            if (request == null)
                throw new NotFoundException("Add cannot be null.");

            var entity = await _repository.GetById(id)
                ?? throw new NotFoundException($"Add with id {id} not found.");

            // Solo sube nueva imagen si se envió una; si no, conserva la URL existente
            if (newImage != null)
            {
                var upload = await cloudinaryService.UploadAsync(newImage, "adds");
                entity.image_url = upload.Url;
            }

            entity.target_link = request.TargetURL;
            entity.active = request.Active;
            entity.updated_at = DateTime.UtcNow;
            entity.updated_by = adminId;

            Console.WriteLine($"Updating add with id: {entity.id}");

            return await _repository.Update(entity);
        }
    }
}