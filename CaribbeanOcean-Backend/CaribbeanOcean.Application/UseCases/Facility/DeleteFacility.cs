using System;

using System.Text;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Facility
{
    public class DeleteFacility(IFacilityRepository facilityRepository)
    {

        public async Task<bool> ExecuteAsync(int id, int? updatedBy = null)
        {
            if (id <= 0)
            {
                throw new ValidationException("Facility id is required.");
            }

            var facility = await facilityRepository.GetFacilityById(id);

            if (facility == null)
            {
                throw new NotFoundException("Facility not found.");
            }

            if (!facility.active)
            {
                throw new ConflictException("Facility is already inactive.");
            }

            return await facilityRepository.DeleteFacility(id, updatedBy);
        }
    }
}
