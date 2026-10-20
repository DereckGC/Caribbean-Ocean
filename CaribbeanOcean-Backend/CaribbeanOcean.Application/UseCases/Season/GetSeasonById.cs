using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Season
{
    public class GetSeasonById (ISeasonRepository seasonRepository)
    {

        public async Task<DTOs.Season.SeasonDTO?> ExecuteAsync(int id)
        {
            var entity = await seasonRepository.getSeasonById(id);
            return entity is null ? null : Mappers.SeasonMapper.ToDto(entity);
        }
    }
}
