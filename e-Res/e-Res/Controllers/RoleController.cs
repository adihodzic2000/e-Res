
using Common.Dto.Role;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_Res.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController:ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController( IRoleService _roleService)
        {
            this._roleService = _roleService;
        }

        [HttpPost("create-role"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoleAsMessageAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken)
        {
            var message = await _roleService.CreateRoleAsMessageAsync(roleCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [AllowAnonymous]
        [HttpDelete("delete-role"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoleAsMessageAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var message = await _roleService.DeleteRoleAsMessageAsync(roleId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPost("add-role-to-user"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUserAsMessageAsync(UserRoleDto userRoleDto, CancellationToken cancellationToken)
        {
            var message = await _roleService.AddRoleToUserAsMessageAsync(userRoleDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
