using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Season
{
    public class GetAllSeasons (ISeasonRepository seasonRepository)
    {
        public async Task<IEnumerable<DTOs.Season.SeasonDTO>> ExecuteAsync()
        {
            var entities = await seasonRepository.getAllSeasons();
            return Mappers.SeasonMapper.ToDtoList(entities);
        }
    }
}
