using Common.Dto;
using Common.Dto.Country;
using Common.Dto.Locations;
using Core.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_Res.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {

        private readonly ILocationService _locationService;

        public LocationController(ILocationService _locationService)
        {
            this._locationService = _locationService;
        }


        [HttpPost("add-location"), Authorize(Roles = "Mobile" + "," + "Desktop" + "," + "Admin")]
        public async Task<IActionResult> AddLocationAsMessageAsync(LocationCreateDto locationCreateDto, CancellationToken cancellationToken)
        {
            var message = await _locationService.CreateLocationAsMessageAsync(locationCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-location"), Authorize(Roles = "Mobile" + "," + "Desktop" + "," + "Admin")]
        public async Task<IActionResult> GetLocationAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _locationService.GetLocationAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpDelete("delete-location"), Authorize(Roles = "Desktop" + "," + "Admin")]
        public async Task<IActionResult> DeleteLocationAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _locationService.DeleteLocationAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPut("update-location"), Authorize(Roles = "Mobile" + "," + "Desktop" + "," + "Admin")]
        public async Task<IActionResult> UpdateLocationAsMessageAsync(Guid Id, LocationUpdateDto locationUpdateDto, CancellationToken cancellationToken)
        {
            var message = await _locationService.UpdateLocationAsMessageAsync(Id, locationUpdateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
