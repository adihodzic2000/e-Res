using Common.Dto.User;
using Common.Dtos.Verification;
using Core.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_Res.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateUserAsMessageAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var message = await _userService.CreateUserAsMessageAsync(userCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _userService.GetUsersAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPut("update-user/{Id}"), Authorize()]
        public async Task<IActionResult> UpdateUserAsMessageAsync(Guid Id, UserUpdateDto user, CancellationToken cancellationToken)
        {
            var message = await _userService.UpdateUserAsMessageAsync(Id, user, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-user/{Id}"), Authorize()]
        public async Task<IActionResult> CheckUserAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _userService.CheckUserAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-my-places"), Authorize()]
        public async Task<IActionResult> GetMyPlacesAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _userService.GetMyPlacesAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassowrdAsMessageAsync(VerificationCreateDto verificationCreateDto, CancellationToken cancellationToken)
        {
            var message = await _userService.ForgotPasswordAsMessageAsync(verificationCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPost("check-code")]
        public async Task<IActionResult> CheckCodeAsMessageAsync(VerificationCodeDto verificationCodeDto, CancellationToken cancellationToken)
        {
            var message = await _userService.CheckCodeAsMessageAsync(verificationCodeDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsMessageAsync(NewPasswordDto newPasswordDto, CancellationToken cancellationToken)
        {
            var message = await _userService.NewPasswordAsMessageAsync(newPasswordDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
