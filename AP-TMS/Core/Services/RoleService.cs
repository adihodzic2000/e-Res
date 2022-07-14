using Core.Dto;
using Core.Dto.Role;
using Core.Interfaces;
using Database;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RoleService : IRoleService
    {
        public readonly AptmsContext _dbContext;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(AptmsContext aptmsContext, RoleManager<Role> roleManager)
        {
            _dbContext = aptmsContext;
            _roleManager = roleManager;
        }

        public async Task<Message> CreateRoleAsMessageAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken)
        {
            Role role = new Role();
            role.Id = new Guid();
            role.Name = roleCreateDto.Name;
            role.Description = roleCreateDto.Description;
            role.IsDeleted = false;
            var _role = await _dbContext.Roles.Where(x => x.Name == role.Name).FirstOrDefaultAsync();
            if (_role== null)
            {
                await _roleManager.CreateAsync(role);
                return new Message
                {
                    IsValid = true,
                    Data = role,
                    Status = Enums.ExceptionCode.Success
                };

            }

            return new Message
            {
                Info="Ista uloga ne može biti dodana više puta!",
                IsValid = false,
                Status = Enums.ExceptionCode.BadRequest
            };

        }

        public async Task<Message> DeleteRoleAsMessageAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == roleId && !x.IsDeleted);
            if (role == null)
            {
                return new Message
                {
                    Info = "Uloga ne postoji!",
                    IsValid = false,
                    Status = Enums.ExceptionCode.NotFound
                };
            }
            role.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return new Message
            {
                Info = "Uloga uspješno obrisana",
                IsValid = true,
                Status = Enums.ExceptionCode.Success
            };
        }
    }
}
