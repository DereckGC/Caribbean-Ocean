using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Add
{
    public class DeleteAdd(IAddRepository _repository)
    {
        public async Task<bool> ExecuteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Offer ID must be positive.");
            }

            return await _repository.Delete(id);
        }
    }
}
