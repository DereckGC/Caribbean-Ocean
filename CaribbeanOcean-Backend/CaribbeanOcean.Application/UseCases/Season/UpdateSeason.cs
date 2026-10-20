using CaribbeanOcean.Application.DTOs.Season;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Season
{
 public class UpdateSeason (ISeasonRepository seasonRepository)
    {
        public async Task <bool>  ExecuteAsync(SeasonDTO dto)
        {

            var existingEntity = await seasonRepository.getSeasonById(dto.Id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Season not found.");
            }

            existingEntity.name = dto.Name;
            existingEntity.startDate = dto.StartDate;
            existingEntity.endDate = dto.EndDate;
            existingEntity.discountAmount = dto.DiscountAmount;

            if (dto.StartDate > dto.EndDate)
                throw new ArgumentException("StartDate cannot be greater than EndDate");

            return await seasonRepository.updateSeason(existingEntity);
        }
    }
}
