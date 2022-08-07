using AutoMapper;
using Common.Dto.Role;
using Core.Interfaces;
using Database;
using Entities;
using Entities.Enums;
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
        public readonly ERESContext _dbContext;
        private readonly RoleManager<Role> _roleManager;
        public IMapper Mapper { get; set; }
        private UserManager<User> UserManager { get; set; }
        public RoleService(ERESContext dbContext, RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            Mapper = mapper;
            UserManager = userManager;
        }


        public async Task<Message> CreateRoleAsMessageAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken)
        {
            var _role = await _dbContext.Roles.Where(x => x.Name == roleCreateDto.Name).FirstOrDefaultAsync(cancellationToken);
            if (_role == null)
            {
                var role = Mapper.Map<Role>(roleCreateDto);
                await _roleManager.CreateAsync(role);
                return new Message
                {
                    IsValid = true,
                    Data = role,
                    Status = ExceptionCode.Success
                };

            }

            return new Message
            {
                Info = "Ista uloga ne može biti dodana više puta!",
                IsValid = false,
                Status = ExceptionCode.BadRequest
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
                    Status = ExceptionCode.NotFound
                };
            }
            role.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new Message
            {
                Info = "Uloga uspješno obrisana",
                IsValid = true,
                Status = ExceptionCode.Success
            };
        }

        public async Task<Message> AddRoleToUserAsMessageAsync(UserRoleDto userRoleDto, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.Where(x => x.Id == userRoleDto.UserId).FirstOrDefaultAsync(cancellationToken);

            if (user != null)
            {
                try
                {
                    var roles = await _dbContext.Roles
                             .Where(x => userRoleDto.RoleIds.Contains(x.Id))
                             .ToListAsync(cancellationToken);

                    await UserManager.AddToRolesAsync(user, roles.Select(x => x.NormalizedName)); //Zanemarit će već postojeće
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfuly added roles",
                        Status = ExceptionCode.Success
                    };
                }
                catch (Exception ex)
                {
                    return new Message
                    {
                        IsValid = false,
                        Info = "Bad request",
                        Status = ExceptionCode.BadRequest
                    };
                }


            }

            return new Message
            {
                Info = "Korisnik ne postoji u bazi!",
                IsValid = false,
                Status = ExceptionCode.BadRequest
            };
        }
    }
}
