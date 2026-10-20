using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Application.UseCases.AboutUs;

namespace CaribbeanOcean.Application.Services
{
    public class AboutUsService (GetAboutUs getAboutUs, UpdateAboutUs updateAboutUs)
    {
        public async Task<AboutUsDTO> Get() => await getAboutUs.ExecuteAsync();
        public async Task<bool> Update(AdminAboutUsDTO dto) => await updateAboutUs.ExecuteAsync(dto);
    }
}
