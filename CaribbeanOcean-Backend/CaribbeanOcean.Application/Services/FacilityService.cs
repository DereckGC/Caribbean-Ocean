using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.UseCases.Facility;
using CaribbeanOcean.Application.UseCases.Locations;

namespace CaribbeanOcean.Application.Services
{
    public class FacilityService(GetPageFacilities getPageFacility, CreateFacility createFacility, UpdateFacility updateFacility, DeleteFacility deleteFacility, GetFacilityById getFacilityById, GetAllFacilities getAllFacilities)
    {
        public async Task<FacilityPageDTO> GetFacilitiesPage() => await getPageFacility.ExecuteAsync();
        public async Task<int> CreateFacility(CreateFacilityDTO createFacilityDTO) => await createFacility.ExecuteAsync(createFacilityDTO);
        public async Task<bool> UpdateFacility(int id, UpdateFacilityDTO updateFacilityDTO) => await updateFacility.ExecuteAsync(updateFacilityDTO);

        public async Task<bool> DeleteFacility (int id, int? updatedBy = null) => await deleteFacility.ExecuteAsync(id, updatedBy);

        public async Task <FacilityDTO> GetFacilityById(int id) => await getFacilityById.ExecuteAsync(id);

        public async Task<IEnumerable<FacilityDTO>> GetFacilityDTOs() => await getAllFacilities.ExecuteAsync();
    }

}
