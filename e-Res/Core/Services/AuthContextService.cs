using Common.Dto.Auth;
using Common.Dtos.Auth;
using Common.JwtConfiguration;
using Core.Interfaces;
using Database;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Http;
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
	public class AuthContextService : IAuthContext
	{
		private readonly UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AuthContextService(UserManager<User> userManager,
			IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<GetLoggedUserDto> GetLoggedUser()
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId == null)
			{
				throw new ArgumentException("Unauthorized");
			}

			var user = await _userManager.FindByIdAsync(userId);

			

			return new GetLoggedUserDto
			{
				Id = Guid.Parse(userId),
				FirstName = user.FirstName,
				LastName = user.LastName,
				CompanyId = user.CompanyId
			};

		}
	}
}
