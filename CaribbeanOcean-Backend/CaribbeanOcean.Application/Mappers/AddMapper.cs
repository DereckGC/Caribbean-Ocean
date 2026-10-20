using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Add;
using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.Mappers
{
    public class AddMapper
    {
        public static AddDTO ToDTO(Add entity)
        {
            if (entity == null) return null!;
            return new AddDTO
            {
                Id = entity.id,
                ImageURL = entity.image_url,
                TargetURL = entity.target_link,
                UpdatedAt = entity.updated_at,
                UpdatedBy = entity.updated_by,
                Active = entity.active
            };
        }

        public static Add ToEntity(AddDTO entity)
        {
            if (entity == null) return null!;
            return new Add
            {
                id = entity.Id,
                image_url = entity.ImageURL,
                target_link = entity.TargetURL,
                updated_at = entity.UpdatedAt,
                updated_by = entity.UpdatedBy,
                active = entity.Active
            };
        }
    }
}
