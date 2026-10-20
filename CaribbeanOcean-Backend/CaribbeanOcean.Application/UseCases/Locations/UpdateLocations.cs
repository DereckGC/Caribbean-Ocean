using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.DTOs.Multimedia;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Locations
{
    public class UpdateLocations(IPageContentRepository pageContentRepository)
    {
        public async Task<bool> ExecuteAsync(AdminLocationsDTO dto)
        {
            var existing = await pageContentRepository.GetPageContentByTitle("locations");

            if (existing == null)
                throw new NotFoundException("Locations content not found.");

            existing.title = dto.Title;
            existing.subtitle = dto.SubTitle;
            existing.description = dto.Description;
            existing.url_content = dto.Images
                    .Select(MultimediaMapper.ToEntity)
                    .ToList();
            existing.updated_by = dto.UpdateBy;

            return await pageContentRepository.UpdatePageContent(existing);
        }
    }
}
