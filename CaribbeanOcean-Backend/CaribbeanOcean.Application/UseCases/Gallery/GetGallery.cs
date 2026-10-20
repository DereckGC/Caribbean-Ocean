using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Gallery;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Gallery
{
    public class GetGallery(IPageContentRepository pageContentRepository)
    {
        public async Task<GalleryDTO> ExecuteAsync()
        {
            var entity = await pageContentRepository.GetPageContentByTitle("gallery");

            if (entity == null)
            {
                throw new NotFoundException("Gallery content not found.");
            }

            return GalleryMapper.ToDTO(entity);

        }
    }
}
