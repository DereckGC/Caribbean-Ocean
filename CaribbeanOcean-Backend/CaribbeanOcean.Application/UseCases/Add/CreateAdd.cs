using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CaribbeanOcean.Application.UseCases.Add
{
    public class CreateAdd(IAddRepository addRepository, ICloudinaryService cloudinaryService)
    {
        public async Task<bool> ExecuteAsync(IFormFile image, string targetURL, int adminId)
        {
            if (image == null)
                throw new NotFoundException("Image cannot be null.");

            if (string.IsNullOrWhiteSpace(targetURL))
                throw new NotFoundException("TargetURL cannot be empty.");

            var upload = await cloudinaryService.UploadAsync(image, "adds");

            var entity = new Domain.Entities.Add
            {
                image_url = upload.Url,
                target_link = targetURL,
                updated_at = DateTime.UtcNow,
                updated_by = adminId,
                active = true
            };

            Console.WriteLine($"Creating add with ImageURL: {entity.image_url}, TargetURL: {entity.target_link}");

            int newId = await addRepository.Create(entity);
            return newId > 0;
        }
    }
}