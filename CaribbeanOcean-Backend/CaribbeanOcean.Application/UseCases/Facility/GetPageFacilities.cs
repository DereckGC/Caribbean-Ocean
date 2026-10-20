using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Facility
{
    public class GetPageFacilities(IFacilityRepository facilityRepository, IPageContentRepository pageContentRepository)
    {
        public async Task<FacilityPageDTO> ExecuteAsync()
        {
            var facilities = await facilityRepository.GetAllFacilities();

            if (facilities == null)
            {
                throw new NotFoundException("Facilities content not found.");
            }
            var pageContent = await pageContentRepository.GetPageContentByTitle("facilities");

            if (facilities == null)
            {
                throw new NotFoundException("Facilities content not found.");
            }
            var page = FacilityMapper.ToPageDTO(pageContent, facilities);
            return page;
            
        }
    }
}
