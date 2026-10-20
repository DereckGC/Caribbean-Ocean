using CaribbeanOcean.Application.UseCases.Season;
using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Season;

namespace CaribbeanOcean.Application.Services
{
    public class SeasonService  (
        CreateSeason createSeason,
        UpdateSeason updateSeason,
        GetAllSeasons getAllSeasons,
        GetSeasonById getSeasonById,
        DeleteSeason  deleteSeason) 
    {
        public async Task<int> Create(CreateSeasonDTO dto) => await createSeason.ExecuteAsync(dto);

        public async Task Update(int id, UpdateSeasonDTO dto)
        {
            var seasonDto = new SeasonDTO
            {
                Id = id,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DiscountAmount = dto.DiscountAmount
            };

            await updateSeason.ExecuteAsync(seasonDto);
        }

        public async Task<SeasonDTO?> GetById(int id) => await getSeasonById.ExecuteAsync(id);

        public async Task<IEnumerable<SeasonDTO>> GetAll() => await getAllSeasons.ExecuteAsync();

        public async Task Delete(int id) => await deleteSeason.ExecuteAsync(id);

   
    }
}
