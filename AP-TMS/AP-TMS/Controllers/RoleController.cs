using Core.Dto;
using Core.Dto.Role;
using Core.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AP_TMS.Controllers
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

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateRoleAsMessageAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken)
        {
            var message = await _roleService.CreateRoleAsMessageAsync(roleCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoleAsMessageAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var message = await _roleService.DeleteRoleAsMessageAsync(roleId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
