using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Application.DTOs.Location;
using CaribbeanOcean.Application.UseCases.AboutUs;
using CaribbeanOcean.Application.UseCases.Hero;
using CaribbeanOcean.Application.UseCases.Locations;

namespace CaribbeanOcean.Application.Services
{
    public class LocationService(GetLocations getLocations, UpdateLocations updateLocations)
    {
        public async Task<LocationsDTO> Get() => await getLocations.ExecuteAsync();
        public async Task<bool> Update(AdminLocationsDTO dto) => await updateLocations.ExecuteAsync(dto);
    }
}
