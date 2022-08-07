using Common.Dto;
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
    public class CountryController : ControllerBase
    {

        private readonly ICountryService _countryService;

        public CountryController(ICountryService _countryService)
        {
            this._countryService = _countryService;
        }


        [HttpPost("add-country"), AllowAnonymous]
        public async Task<IActionResult> AddCountry(CountryCreateDto createCountryDto, CancellationToken cancellationToken)
        {
            var message = await _countryService.CreateCountryAsMessageAsync(createCountryDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-country"), AllowAnonymous]
        public async Task<IActionResult> GetCountry(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _countryService.GetCountryAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-countries"), AllowAnonymous]
        public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
        {
            var message = await _countryService.GetCountriesAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
