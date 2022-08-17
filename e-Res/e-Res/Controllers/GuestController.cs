using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Core.Interfaces;
using Core.SearchObjects;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_Res.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GuestController : ControllerBase
    {

        private readonly IGuestsService _guestsService;

        public GuestController(IGuestsService _guestsService)
        {
            this._guestsService = _guestsService;
        }


        [HttpPost("add-guest"), Authorize]
        public async Task<IActionResult> AddGuestAsMessageAsync(GuestCreateDto guestCreateDto, CancellationToken cancellationToken)
        {
            var message = await _guestsService.CreateGuestAsMessageAsync(guestCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        
        [HttpGet("get-guest"), Authorize]
        public async Task<IActionResult> GetGuestAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _guestsService.GetGuestAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        
        [HttpPut("update-guest/{Id}"), Authorize]
        public async Task<IActionResult> UpdateGuestAsMessageAsync(Guid Id, GuestUpdateDto guestUpdateDto, CancellationToken cancellationToken)
        {
            var message = await _guestsService.UpdateGuestAsMessageAsync(Id, guestUpdateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        
        [HttpDelete("delete-guest/{Id}"), Authorize]
        public async Task<IActionResult> DeleteGuestAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _guestsService.DeleteGuestAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpPost("get-guest-by-company-id"), Authorize]
        public async Task<IActionResult> GetGuestByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken)
        {
            var message = await _guestsService.GetGuestByCompanyIdAsMessageAsync(baseSearch, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
