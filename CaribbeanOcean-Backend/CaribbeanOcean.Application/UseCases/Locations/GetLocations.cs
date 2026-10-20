using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Locations
{
    public class GetLocations(IPageContentRepository pageContentRepository)
    {
        public async Task<LocationsDTO> ExecuteAsync()
        {
            var entity = await pageContentRepository.GetPageContentByTitle("locations");

            if (entity == null)
            {
                throw new NotFoundException("Locations content not found.");
            }

            return LocationsMapper.ToDTO(entity);

        }
    }
}
