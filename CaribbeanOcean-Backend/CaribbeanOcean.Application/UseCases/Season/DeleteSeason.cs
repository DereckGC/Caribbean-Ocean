using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Season
{
    public  class DeleteSeason (ISeasonRepository seasonRepository)
    {
        public async Task ExecuteAsync(int id)
        {
            await seasonRepository.deleteSeason(id);
        }
    }
}
