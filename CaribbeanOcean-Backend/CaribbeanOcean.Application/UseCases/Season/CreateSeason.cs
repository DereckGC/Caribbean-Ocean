using System.Threading.Tasks;
using CaribbeanOcean.Application.DTOs.Season;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;


namespace CaribbeanOcean.Application.UseCases.Season
{
    public class CreateSeason (ISeasonRepository seasonRepository)
    {
        public async Task<int> ExecuteAsync(CreateSeasonDTO dto)
        {
            return await seasonRepository.createSeason(SeasonMapper.ToEntity(dto));
        }
    }
}
