using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Gallery;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.UseCases.Gallery;
using CaribbeanOcean.Application.UseCases.Locations;

namespace CaribbeanOcean.Application.Services
{
    public class GalleryService(GetGallery getGallery)
    {
        public async Task<GalleryDTO> Get() => await getGallery.ExecuteAsync();
    }
}
