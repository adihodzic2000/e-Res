using Core.Dto.User;
using Core.Enums;
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
    public class UserService : IUserService
    {
        public readonly AptmsContext _dbContext;
        private UserManager<User> UserManager { get; set; }

        public UserService(AptmsContext aptmsContext, UserManager<User> userManager)
        {
            _dbContext = aptmsContext;
            UserManager = userManager;
        }

        public async Task<Message> CreateUserAsMessageAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var status = ExceptionCode.Success;
            try
            {
                bool duplicateEmail = await ThrowOnCreateUserDuplicateEmailError(userCreateDto, cancellationToken);
                bool duplicateUserName = await ThrowOnCreateDuplicateUsernameError(userCreateDto, cancellationToken);
                if (duplicateEmail && duplicateUserName)
                {
                    status = ExceptionCode.BadRequest;
                    throw new Exception($"Korisnik sa e-mailom: '{userCreateDto.Email}' već postoji!#Korisnik sa username:'{userCreateDto.UserName}' već postoji");
                }

                if (duplicateEmail)
                {
                    status = ExceptionCode.BadRequest;
                    throw new Exception($"Korisnik sa e-mailom: '{userCreateDto.Email}' već postoji!");
                }

                if (duplicateUserName)
                {
                    status = ExceptionCode.BadRequest;
                    throw new Exception($"Korisnik sa username: '{userCreateDto.UserName}' već postoji");
                }
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    var user = new User();
                    user.Email = userCreateDto.Email;
                    user.UserName = userCreateDto.UserName;
                    user.PhoneNumber = userCreateDto.PhoneNumber;
                    user.LastName = userCreateDto.LastName;
                    user.FirstName = userCreateDto.FirstName;
                    user.Gender = userCreateDto.Gender;
                    user.CreatedDate=DateTime.Now;
                    user.IsDeleted = false;

                    await UserManager.CreateAsync(user, userCreateDto.Password);
                    var roles = await _dbContext.Roles
                           .Where(x => userCreateDto.UserRoles.Contains(x.Id))
                           .ToListAsync(cancellationToken);

                    await UserManager.AddToRolesAsync(user, roles.Select(x => x.NormalizedName));

                    //var person = new Person();
                    //person.FirstName = userCreateDto.FirstName;
                    //person.LastName = userCreateDto.LastName;
                    //person.Gender = userCreateDto.Gender;
                    //person.CreatedDate = DateTime.Now;
                    //person.IsDeleted = false;
                    //person.UserId = user.Id;
                    //person.User = user;
                    //await _dbContext.Persons.AddAsync(new Person
                    //{
                    //    FirstName = userCreateDto.FirstName,
                    //    LastName = userCreateDto.LastName,
                    //    Gender = userCreateDto.Gender,
                    //    CreatedDate = DateTime.Now,
                    //    IsDeleted = false,
                    //    User = user
                    //});
                    //await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new Message { Info = ex.Message, IsValid = false, Status = status };
                }
                return new Message { Info = "Successfully created user", IsValid = true, Status = status };
            }
            catch (Exception ex)
            {
                status = ExceptionCode.BadRequest;
                return new Message { Info = ex.Message, IsValid = false, Status = status };
            }
        }

        private async Task<bool> ThrowOnCreateUserDuplicateEmailError(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {

            var emailExists = await _dbContext.Users.
                AnyAsync(x => x.Email == userCreateDto.Email, cancellationToken);

            if (emailExists)
                return true;

            else
                return false;
        }

        private async Task<bool> ThrowOnCreateDuplicateUsernameError(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {

            var usernameExists = await _dbContext.Users.
                AnyAsync(x => x.UserName == userCreateDto.UserName, cancellationToken);

            if (usernameExists)
                return true;

            else
                return false;
        }
    }
}
