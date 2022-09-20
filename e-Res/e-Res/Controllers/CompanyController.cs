using Common.Dto;
using Common.Dto.Company;
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
    public class CompanyController : ControllerBase
    {

        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService _companyService)
        {
            this._companyService = _companyService;
        }


        [HttpPost("add-company"), Authorize]
        public async Task<IActionResult> AddCompany(CompanyCreateDto companyCreateDto, CancellationToken cancellationToken)
        {
            var message = await _companyService.CreateCompanyAsMessage(companyCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-company/{Id}"), Authorize]
        public async Task<IActionResult> GetCompany(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _companyService.GetCompanyAsMessage(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-companies-recommender/{CountryId}/{CityId}/{IsApartment}/{IsHotel}"), Authorize]
        public async Task<IActionResult> GetCompaniesRecommenderAsMessageAsync(Guid CountryId, Guid CityId, bool IsApartment, bool IsHotel, CancellationToken cancellationToken)
        {
            var message = await _companyService.GetCompaniesRecommenderAsMessage(CountryId, CityId, IsApartment, IsHotel, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-companies/{CountryId}/{CityId}/{IsApartment}/{IsHotel}"), Authorize]
        public async Task<IActionResult> GetCompaniesAsMessageAsync(Guid CountryId, Guid CityId, bool IsApartment, bool IsHotel, CancellationToken cancellationToken)
        {
            var message = await _companyService.GetCompaniesAsMessage(CountryId, CityId, IsApartment, IsHotel, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPut("update-company/{Id}"), Authorize]
        public async Task<IActionResult> UpdateCompany(Guid Id, CompanyUpdateDto companyUpdateDto, CancellationToken cancellationToken)
        {
            var message = await _companyService.UpdateCompanyAsMessage(Id, companyUpdateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }


        //DELETED METHODS
        //[HttpPost("add-user-to-company"), AllowAnonymous]
        //public async Task<IActionResult> AddCompany(AddUserToCompanyDto addUserToCompanyDto, CancellationToken cancellationToken)
        //{
        //    var message = await _companyService.AddUserToCompanyAsMessage(addUserToCompanyDto, cancellationToken);
        //    if (message.IsValid == false)
        //        return BadRequest(message);
        //    return Ok(message);
        //}
    }
}
