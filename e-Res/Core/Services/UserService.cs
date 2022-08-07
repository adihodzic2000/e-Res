using AutoMapper;
using Common.Dto.User;
using Common.Dtos.Verification;
using Core.Interfaces;
using Database;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        public readonly ERESContext _dbContext;
        private UserManager<User> UserManager { get; set; }
        public IMapper Mapper { get; set; }

        public UserService(ERESContext dbContext, UserManager<User> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            UserManager = userManager;
            Mapper = mapper;
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
                    user.CreatedDate = DateTime.Now;
                    user.CompanyId = userCreateDto.CompanyId;
                    user.IsDeleted = false;

                    await UserManager.CreateAsync(user, userCreateDto.Password);
                    var roles = await _dbContext.Roles
                           .Where(x => userCreateDto.UserRoles.Contains(x.Id))
                           .ToListAsync(cancellationToken);

                    await UserManager.AddToRolesAsync(user, roles.Select(x => x.NormalizedName));

                    await transaction.CommitAsync(cancellationToken);
                    return new Message { Info = "Successfully created user", IsValid = true, Status = status };
                }
                catch (Exception ex)
                {
                    status = ExceptionCode.BadRequest;
                    await transaction.RollbackAsync(cancellationToken);
                    return new Message { Info = ex.Message, IsValid = false, Status = status };
                }
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

        public async Task<Message> GetUsersAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _dbContext.Users.Where(x => x.Id != new Guid("CEECE2E6-4966-4652-A711-08DA6B3BD31E")).ToListAsync();

                if (users != null)
                {
                    var _obj = Mapper.Map<List<UserGetDto>>(users);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got users",
                        Status = ExceptionCode.Success,
                        Data = _obj
                    };
                }
                return new Message
                {
                    IsValid = true,
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = ex.Message,
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public bool _SendMail(string To, string Subject, string Sadrzaj)
        {
            MailMessage message = new MailMessage("mverifikacija@gmail.com", To);
            message.Subject = Subject;
            message.Body = Sadrzaj;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("mverifikacija@gmail.com", "kvajdweznozeynay");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Message> ForgotPasswordAsMessageAsync(VerificationCreateDto verificationCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Users.Where(x => x.Email == verificationCreateDto.Email).FirstOrDefaultAsync(cancellationToken);
                if (user == null)
                    return new Message { Info = "Korisnik nije nađen!", Status = ExceptionCode.NotFound, IsValid = false };
                var guid = Guid.NewGuid().ToString();
                var code = guid.Substring(0, 6);
                Verifications verification = new Verifications()
                {
                    Id=Guid.NewGuid(),
                    Code = code,
                    ExpireDate = DateTime.Now.AddMinutes(30),
                    IsConfirmed = false,
                    UserId = user.Id,
                };
                await _dbContext.Verifications.AddAsync(verification);
                await _dbContext.SaveChangesAsync(cancellationToken);
                bool n=_SendMail(verificationCreateDto.Email, "Verifikacijski kod", code);
                if(n)
                return new Message { Info = "Uspješno vraćen kod", IsValid = true, Status = ExceptionCode.Success };
                else return new Message { Info = "Greška", IsValid = false, Status = ExceptionCode.BadRequest };

            }
            catch (Exception ex)
            {
                return new Message { Info = "Greška", IsValid = false, Status = ExceptionCode.BadRequest };
            }
        }

        public async Task<Message> CheckCodeAsMessageAsync(VerificationCodeDto verificationCodeDto, CancellationToken cancellationToken)
        {
            try
            {
                var verification = await _dbContext.Verifications.Include(x => x.User).Where(x => x.Code == verificationCodeDto.Code && x.User.Email == verificationCodeDto.Email && !x.IsConfirmed).FirstOrDefaultAsync();
                if(verification == null)
                    return new Message { Info = "Greška", IsValid = false, Status = ExceptionCode.BadRequest };
                else
                {
                    verification.IsConfirmed = true;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    return new Message { Info = "Uspješno potvrđen kod", Data=verification.UserId, IsValid = true, Status = ExceptionCode.Success };
                }
            }
            catch (Exception ex)
            {
                return new Message { Info = "Greška", IsValid = false, Status = ExceptionCode.BadRequest };
            }
        }

        public async Task<Message> NewPasswordAsMessageAsync(NewPasswordDto newPasswordDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Users.Where(x => x.Id == newPasswordDto.Id).FirstOrDefaultAsync(cancellationToken);
                await UserManager.RemovePasswordAsync(user);
                await UserManager.AddPasswordAsync(user, newPasswordDto.Password);
                return new Message { Info = "Uspješno izmjenjena šifra", IsValid = true, Status = ExceptionCode.Success };
            }
            catch(Exception ex)
            {
                return new Message { Info = "Greška", IsValid = false, Status = ExceptionCode.BadRequest };
            }
        }
    }
}
