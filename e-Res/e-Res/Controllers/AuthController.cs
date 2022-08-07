using Common.Dto.Auth;

using Core.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_Res.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController:ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private IAuthService _authService;

        public AuthController(UserManager<User> userManager, IAuthService _authService)
        {
            _userManager = userManager;

            this._authService = _authService;
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login, CancellationToken cancellationToken)
        {
            var message = await _authService.Login(login, cancellationToken);
            if (!message.IsValid)
                return Unauthorized(message);

            (SessionDto Session, string RefreshToken) authData = ((SessionDto, string))message.Data;

            Response.Cookies.Append("X-Refresh-Token", authData.RefreshToken, new CookieOptions() { HttpOnly = false, SameSite = SameSiteMode.None, Secure = true, IsEssential = true });


            return Ok(authData.Session);
        }
        [Authorize, HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _authService.LogoutAsync(user);
            return Ok();
        }
    }
}
