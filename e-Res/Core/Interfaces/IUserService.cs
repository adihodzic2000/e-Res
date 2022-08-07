using Common.Dto.User;
using Common.Dtos.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<Message> CreateUserAsMessageAsync(UserCreateDto user, CancellationToken cancellationToken);
        Task<Message> GetUsersAsMessageAsync(CancellationToken cancellationToken);
        Task<Message> ForgotPasswordAsMessageAsync(VerificationCreateDto verificationCreateDto, CancellationToken cancellationToken);
        Task<Message> CheckCodeAsMessageAsync(VerificationCodeDto verificationCodeDto, CancellationToken cancellationToken);
        Task<Message> NewPasswordAsMessageAsync(NewPasswordDto newPasswordDto, CancellationToken cancellationToken);
    }
}
