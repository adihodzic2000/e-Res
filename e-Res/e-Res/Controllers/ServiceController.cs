using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dtos.Services;
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
    public class ServiceController : ControllerBase
    {

        private readonly IServicesService _servicesService;

        public ServiceController(IServicesService _servicesService)
        {
            this._servicesService = _servicesService;
        }


        [HttpPost("add-service")]
        public async Task<IActionResult> AddServiceAsMessageAsync(ServicesCreateDto  servicesCreateDto, CancellationToken cancellationToken)
        {
            var message = await _servicesService.CreateServiceAsMessageAsync(servicesCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        
        [HttpGet("get-service/{Id}")]
        public async Task<IActionResult> GetServiceAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _servicesService.GetServiceAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        
        [HttpPut("update-service/{Id}")]
        public async Task<IActionResult> UpdateServiceAsMessageAsync(Guid Id, ServicesUpdateDto  servicesUpdateDto, CancellationToken cancellationToken)
        {
            var message = await _servicesService.UpdateServiceAsMessageAsync(Id, servicesUpdateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        
        [HttpDelete("delete-service/{Id}")]
        public async Task<IActionResult> AddCompany(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _servicesService.DeleteServiceAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpPost("get-services-by-company-id")]
        public async Task<IActionResult> GetServiceByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken)
        {
            var message = await _servicesService.GetServicesByCompanyIdAsMessageAsync(baseSearch, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpPost("add-service-to-guest")]
        public async Task<IActionResult> AddServiceToGuestAsMessageAsync(AddServiceToGuest addServiceToGuest, CancellationToken cancellationToken)
        {
            var message = await _servicesService.AddServiceToGuestIdAsMessageAsync(addServiceToGuest, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
