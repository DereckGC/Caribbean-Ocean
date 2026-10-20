using CaribbeanOcean.Application.DTOs.Admin;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaribbeanOcean.Application.UseCases.Authentication
{
    public class Register(IAdminUserRepository _repository)
    {
        private readonly PasswordHasher<AdminUser> _hasher = new();

        public async Task<int> ExecuteAsync(RegisterRequestDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var existingUser = await _repository.GetByUsername(dto.Username);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            if (dto.Password.Length < 6)
            {
                throw new ValidationException("The password must be at least 6 characters long.");
            }

            var tempUser = new AdminUser();  
            var passwordHash = _hasher.HashPassword(tempUser!, dto.Password);

            var user = AdminMapper.ToEntity(dto, passwordHash);
            return await _repository.AddAdminUser(user);
        }

    }
}
