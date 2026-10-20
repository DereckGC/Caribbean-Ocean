using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.AboutUs
{
    public class GetAboutUs(IPageContentRepository pageContentRepository)
    {
        public async Task<AboutUsDTO> ExecuteAsync()
        {
            var entity = await pageContentRepository.GetPageContentByTitle("about_us");

            if (entity == null)
            {
                throw new NotFoundException("AboutUs content not found.");
            }

            return AboutUsMapper.ToDTO(entity);
        }
    }
}
