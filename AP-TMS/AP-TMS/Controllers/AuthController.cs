using Core.Dto;
using Core.Dto.Role;
using Core.Dto.User;
using Core.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AP_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController:ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService _authService)
        {
            this._authService = _authService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login, CancellationToken cancellationToken)
        {
            var message = await _authService.Login(login, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
       
    }
}
