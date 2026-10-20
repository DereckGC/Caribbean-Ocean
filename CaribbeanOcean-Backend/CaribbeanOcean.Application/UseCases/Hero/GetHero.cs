using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Hero
{
    public class GetHero(IPageContentRepository pageContentRepository)
    {
        public async Task<HeroDTO> ExecuteAsync()
        {
            var entity = await pageContentRepository.GetPageContentByTitle("hero");

            if (entity == null)
            {
                throw new NotFoundException("Hero content not found.");
            }

            return HeroMapper.ToDTO(entity);

        }
    }
}
