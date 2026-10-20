using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Application.DTOs.Multimedia;
using CaribbeanOcean.Application.UseCases.Hero;

namespace CaribbeanOcean.Application.Services
{
    public class HeroService(
        GetHero getHero,
        GetHeroImages getHeroImages,
        AddHeroImage addHeroImage,
        UpdateHeroImage updateHeroImage,
        DeleteHeroImage deleteHeroImage,
        UpdateHeroText updateHeroText
        )
    {
        public async Task<HeroDTO> Get() => await getHero.ExecuteAsync();

        public async Task<IEnumerable<MultimediaDTO>> GetImages() => await getHeroImages.ExecuteAsync();

        public async Task AddImage(AddHeroImageDTO dto) => await addHeroImage.ExecuteAsync(dto);

        public async Task UpdateImage(int imageId, UpdateHeroImageDTO dto) => await updateHeroImage.ExecuteAsync(imageId, dto);

        public async Task DeleteImage(int imageId) => await deleteHeroImage.ExecuteAsync(imageId);

        public async Task UpdateText(UpdateHeroTextDTO dto) => await updateHeroText.ExecuteAsync(dto);
    }
}