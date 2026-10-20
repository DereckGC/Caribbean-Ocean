
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Facility
{
    public class CreateFacility(IFacilityRepository facilityRepository)
    {
     
        public async Task<int> ExecuteAsync(CreateFacilityDTO dto)
        {
            Validate(dto);
            var facility = FacilityMapper.ToEntityCreate(dto);
            return await facilityRepository.CreateFacility(facility);
        }


        private static void Validate(CreateFacilityDTO dto)
        {
            if (dto == null)
            {
                throw new ValidationException("Facility data is required");
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ValidationException("Facility name is required");
            }
            if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 500)
            {
                throw new ValidationException("Facility description cannot exceed 500 characters.");
            }
           
            if (dto.DisplayOrder < 0)
            {
                throw new ValidationException("Display order cannot be negative.");
            }
            if (dto.Features == null)
            {
                dto.Features = new List<string>();
            }

        }
    }
}
