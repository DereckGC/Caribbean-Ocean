using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface ISeasonRepository
    {
        Task<Season?> getSeasonById(int id);
        Task<IEnumerable<Season>> getAllSeasons();
        Task<int> createSeason(Season season);

        Task<bool> updateSeason(Season season);

        Task<bool> deleteSeason(int id);

        Task<Season?> GetActiveSeasonByDate(DateTime date);
    }
}
