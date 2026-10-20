using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Add;
using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.Add
{
    public class GetAdds(IAddRepository _repository)
    {
        public async Task<IEnumerable<AddDTO>> ExecuteAsync()
        {
            var adds = await _repository.GetAll();

            return adds.Select(add => AddMapper.ToDTO(add));
        }
    }
}
