using Common;
using Core.Dto.Auth;
using Core.Dto.Role;
using Core.Enums;
using Core.Interfaces;
using Database;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private JwtConfiguration _jwtConfiguration { get; set; }
        private UserManager<User> _userManager { get; set; }
        private SignInManager<User> _signInManager { get; set; }
        private AptmsContext _dbContext { get; set; }


        public AuthService(JwtConfiguration jwtConfiguration,
         UserManager<User> userManager,
         SignInManager<User> signInManager,
         AptmsContext dbContext)
        {
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<Message> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username, cancellationToken);

            if (user is null)
            {
                return new Message
                {
                    Info = "Forbidden",
                    IsValid = false,
                    Status = ExceptionCode.Forbidden
                };
            }

            var userSignInResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (userSignInResult)
            {
                var roles = await _userManager.GetRolesAsync(user);

                //var permissions = await GetPermissionsByRoles(roles, cancellationToken);

                (string accessToken, long expiresIn) = GenerateJwt(user, roles);

                var refreshToken = SetRefreshToken(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var session = await GetSession(user.Id, accessToken, expiresIn, cancellationToken);

                return new Message
                {
                    Info = "Success",
                    IsValid = true,
                    Status = ExceptionCode.Success,
                    Data = (session, refreshToken)
                };
            }

            return new Message
            {
                Info = "Forbidden",
                IsValid = false,
                Status = ExceptionCode.Forbidden
            };
        }

        public async Task<Message> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return new Message
                {
                    Info = "Forbidden",
                    IsValid = false,
                    Status = ExceptionCode.Forbidden
                };
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken, cancellationToken);
            if (user is null)
            {
                return new Message
                {
                    Info = "Forbidden",
                    IsValid = false,
                    Status = ExceptionCode.Forbidden
                };
            }
            else if (user.RefreshTokenExpireDate <= DateTime.UtcNow)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpireDate = DateTime.MinValue;
                await _dbContext.SaveChangesAsync(CancellationToken.None);

                return new Message
                {
                    Info = "Forbidden",
                    IsValid = false,
                    Status = ExceptionCode.Forbidden
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            //var permissions = await GetPermissionsByRoles(roles, cancellationToken);

            (string accessToken, long expiresIn) = GenerateJwt(user, roles);

            var session = await GetSession(user.Id, accessToken, expiresIn, cancellationToken);

            return new Message
            {
                Info = "Success",
                IsValid = true,
                Status = ExceptionCode.Success,
                Data = session
            };
        }

        private (string Token, long ExpiresIn) GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Adding role claims. 
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            // Adding permission claims. 
            //var permissionClaims = permissions.Select(x => new Claim(CustomClaimTypes.Permission, x));
            //claims.AddRange(permissionClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtConfiguration.ExpirationAccessTokenInMinutes));

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Issuer,
                (IEnumerable<System.Security.Claims.Claim>)claims,
                expires: expires,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), new DateTimeOffset(expires).ToUnixTimeSeconds());
        }

        private string SetRefreshToken(User user)
        {
            user.RefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            user.RefreshTokenExpireDate = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpirationRefreshTokenInMinutes);
            return user.RefreshToken;
        }

        public async Task<Message> LogoutAsync(User user)
        {
            await _signInManager.SignOutAsync();

            user.RefreshToken = null;
            user.RefreshTokenExpireDate = DateTime.MinValue;
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return new Message
            {
                Info = "Success",
                IsValid = true,
                Status = ExceptionCode.Success,
            };
        }

        private async Task<SessionDto> GetSession(Guid userId, string accessToken, long accessTokenExpiration, CancellationToken cancellationToken)
        {
            var websiteUser = await _dbContext.Users
                .Select(x => new
                {
                    UserId = x.Id,
                    x.FirstName,
                    x.LastName
                })
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            var session = new SessionDto()
            {
                UserId = userId,
                FirstName = websiteUser?.FirstName,
                LastName = websiteUser?.LastName,
                Token = accessToken,
                TokenExpireDate = accessTokenExpiration
            };

            return session;
        }
    }
}
