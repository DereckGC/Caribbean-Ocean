using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IAddRepository
    {
        Task<int> Create(Add add);
        Task<IEnumerable<Add>> GetAll();
        Task<Add> GetById(int id);
        Task<bool> Update(Add add);
        Task<bool> Delete(int id);

    }
}
