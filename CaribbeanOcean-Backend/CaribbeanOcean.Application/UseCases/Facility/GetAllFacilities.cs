using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Facility
{
    public class GetAllFacilities(IFacilityRepository facilityRepository)
    {
        public async Task<IEnumerable<FacilityDTO>> ExecuteAsync()
        {
            var facilities = await facilityRepository.GetAllFacilities();

            return FacilityMapper.ToDTOList(facilities);
        }
    }
}
