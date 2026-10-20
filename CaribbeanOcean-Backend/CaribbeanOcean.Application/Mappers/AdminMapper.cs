using CaribbeanOcean.Application.DTOs.Admin;
using CaribbeanOcean.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace CaribbeanOcean.Application.Mappers
{
    public class AdminMapper
    {
        public static AdminUser ToEntity(RegisterRequestDTO dto, string passwordHash) =>
            AdminUser.Create(dto.Username, dto.Name, dto.Email, passwordHash);

        public static RegisterRequestDTO ToDTO(AdminUser entity) => new()
        {
            Username = entity.username,
            Name = entity.name,
            Email = entity.email    
        };
    }
}
