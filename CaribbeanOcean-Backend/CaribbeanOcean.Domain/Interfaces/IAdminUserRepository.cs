using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IAdminUserRepository
    {
        Task<int> AddAdminUser(AdminUser user);

        Task UpdateLastLogin(AdminUser user);

        Task<AdminUser> GetByUsername(string username);
    }
}
