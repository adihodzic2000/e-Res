using Common.Dto.Auth;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<Message> Login(LoginDto loginDto, CancellationToken cancellationToken);
        Task<Message> RefreshToken(string refreshToken, CancellationToken cancellationToken);
        Task<Message> LogoutAsync(User user);
    }
}
