using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Facility
{
    public class UpdateFacility(IFacilityRepository facilityRepository)
    {

        public async Task<bool> ExecuteAsync (UpdateFacilityDTO dto)
        {
            Validate(dto);
            var existingFacility = await facilityRepository.GetFacilityById(dto.Id);
            if(existingFacility == null)
            {
                throw new NotFoundException($"Facility with id {dto.Id} not found.");
            }
            var facility = FacilityMapper.ToEntity(dto);
            return await facilityRepository.UpdateFacility(facility);


        }

        private static void Validate(UpdateFacilityDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Facility data is required.");
            }

            if (dto.Id <= 0)
            {
                throw new ArgumentException("Facility id is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Facility name is required.");
            }

            if (dto.Name.Length > 100)
            {
                throw new ArgumentException("Facility name cannot exceed 100 characters.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 500)
            {
                throw new ArgumentException("Facility description cannot exceed 500 characters.");
            }

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && dto.ImageUrl.Length > 255)
            {
                throw new ArgumentException("Facility image URL cannot exceed 255 characters.");
            }

            if (dto.DisplayOrder < 0)
            {
                throw new ArgumentException("Display order cannot be negative.");
            }
        }
    }
    
    
}
