using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.Mappers
{
    public class FacilityMapper
    {
        public static FacilityDTO ToDTO(Facility facility)
        {
            if (facility == null) return null!;

            return new FacilityDTO
            {
                Id = facility.id,
                Name = facility.name,
                Description = facility.description,
                Features = string.IsNullOrWhiteSpace(facility.features) ? new List<string>()
                 : JsonSerializer.Deserialize<List<string>>(facility.features) ?? new List<string>(),
                Image_Url = facility.image_url,
                Updated_at = facility.updated_at,
                Updated_By = facility.updated_by,
                DisplayOrder = facility.displayOrder,
                Active = facility.active
            };
        }

        public static List<FacilityDTO> ToDTOList(IEnumerable<Facility> facilities)
        {
            if (facilities == null) return null!;
            List<FacilityDTO> dtoList = new List<FacilityDTO>();
            foreach (var facility in facilities)
            {
                dtoList.Add(ToDTO(facility));
            }
            return dtoList;
        }

        public static FacilityPageDTO ToPageDTO(PageContent pageContent, IEnumerable<Facility> facilities)
        {
            if (pageContent == null || facilities == null) return null!;
            return new FacilityPageDTO
            {
                Title = pageContent.title,
                Subtitle = pageContent.subtitle,
                Facilities = ToDTOList(facilities)
            };
        }

        public static Facility ToEntity(FacilityDTO dto)
        {
            if (dto == null) return null!;
            return new Facility
            {
                id = dto.Id,
                name = dto.Name,
                description = dto.Description,
                features = dto.Features != null
                   ? JsonSerializer.Serialize(dto.Features)
                   : "[]",
                image_url = dto.Image_Url,
                updated_at = dto.Updated_at,
                updated_by = dto.Updated_By,
                displayOrder = dto.DisplayOrder,
                active = dto.Active
            };
        }
        public static Facility ToEntityCreate(CreateFacilityDTO dto)
        {

            var cleanFeatures = dto.Features?
                .Where(feature => !string.IsNullOrWhiteSpace(feature))
                .Select(feature => feature.Trim())
                .ToList() ?? new List<string>();


            return new Facility
            {
                name = dto.Name.Trim(),
                description = dto.Description?.Trim() ?? string.Empty,
                features = JsonSerializer.Serialize(cleanFeatures),
                image_url = dto.ImageUrl?.Trim() ?? string.Empty,
                displayOrder = dto.DisplayOrder,
                updated_by = dto.UpdatedBy,
                active = true
            };
        }


        public static Facility ToEntity(UpdateFacilityDTO dto)
        {
            var cleanFeatures = dto.Features?
                .Where(feature => !string.IsNullOrWhiteSpace(feature))
                .Select(feature => feature.Trim())
                .ToList() ?? new List<string>();

            return new Facility
            {
                id = dto.Id,
                name = dto.Name.Trim(),
                description = dto.Description?.Trim() ?? string.Empty,
                features = JsonSerializer.Serialize(cleanFeatures),
                image_url = dto.ImageUrl?.Trim() ?? string.Empty,
                displayOrder = dto.DisplayOrder,
                updated_by = dto.updated_by,
                active = dto.Active
            };
        }
    }
}
