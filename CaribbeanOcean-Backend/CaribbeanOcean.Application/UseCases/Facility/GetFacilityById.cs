using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Facility
{
    public class GetFacilityById(IFacilityRepository facilityRepository)
    {
        public async Task<FacilityDTO> ExecuteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Facility id is required.");
            }

            var facility = await facilityRepository.GetFacilityById(id);

            if (facility == null)
            {
                throw new KeyNotFoundException("Facility not found.");
            }

            return FacilityMapper.ToDTO(facility);
        }
    }
}
