using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IFacilityRepository
    {
        Task<IEnumerable<Facility>> GetAllFacilities();

        Task<IEnumerable<Facility>> GetPublicFacilities();

        Task<Facility?> GetFacilityById(int id);

        Task<int> CreateFacility(Facility facility);

        Task<bool> UpdateFacility(Facility facility);

        Task<bool> DeleteFacility(int id, int? updatedBy = null);

        Task<bool> ActivateFacility(int id, int? updatedBy = null);
    }
}
