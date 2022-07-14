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
    public class UserController:ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsMessageAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var message = await _userService.CreateUserAsMessageAsync(userCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
       
    }
}
