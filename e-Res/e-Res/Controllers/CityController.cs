using Common.Dto;
using Common.Dto.City;
using Common.Dto.Country;
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
    public class CityController : ControllerBase
    {

        private readonly ICityService _cityService;

        public CityController(ICityService _cityService)
        {
            this._cityService = _cityService;
        }

      
        [HttpPost("add-city"), Authorize(Roles = "Desktop" + "," + "Mobile"+","+"Admin")]
        public async Task<IActionResult> AddCity(CityCreateDto cityCreateDto, CancellationToken cancellationToken)
        {
            var message = await _cityService.CreateCityAsMessageAsync(cityCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-city"), Authorize(Roles = "Desktop" + "," + "Mobile" + "," + "Admin")]
        public async Task<IActionResult> GetCity(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _cityService.GetCityAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-cities-by-country-id/{Id}"), Authorize()]
        public async Task<IActionResult> GetCities(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _cityService.GetCitesByCountryIdAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
