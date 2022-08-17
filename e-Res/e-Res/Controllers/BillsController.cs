using Common.Dto;
using Common.Dto.Bills;
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
    public class BillsController : ControllerBase
    {

        private readonly IBillService _billService;

        public BillsController(IBillService _billService)
        {
            this._billService = _billService;
        }


        [HttpPost("create-bill"), Authorize(Roles = "Desktop")]
        public async Task<IActionResult> CreateBillAsMessageAsync(BillsCreateDto billsCreateDto, CancellationToken cancellationToken)
        {
            var message = await _billService.CreateBillAsMessageAsync(billsCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-bill/{BillId}"), Authorize(Roles = "Desktop")]
        public async Task<IActionResult> GetBillAsMessageAsync(Guid BillId, CancellationToken cancellationToken)
        {
            var message = await _billService.GetBillAsMessageAsync(BillId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-bills-by-logged-user"), Authorize(Roles = "Mobile")]
        public async Task<IActionResult> GetBillsByLoggedUserAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _billService.GetBillsByLoggedUserAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-bill-details/{BillId}"), Authorize]
        public async Task<IActionResult> GetBillDetailsAsMessageAsync(Guid BillId, CancellationToken cancellationToken)
        {
            var message = await _billService.GetBillDetailsAsMessageAsync(BillId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPut("update-bill/{BillId}"), Authorize(Roles = "Desktop")]
        public async Task<IActionResult> UpdateBillAsMessageAsync(Guid BillId, BillsUpdateDto billsUpdateDto, CancellationToken cancellationToken)
        {
            var message = await _billService.UpdateBillAsMessageAsync(BillId, billsUpdateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpDelete("delete-bill/{BillId}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBillAsMessageAsync(Guid BillId, CancellationToken cancellationToken)
        {
            var message = await _billService.DeleteBillAsMessageAsync(BillId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpPost("get-bills-by-company-id"), Authorize]
        public async Task<IActionResult> GetBillsByCompanyIdAsMessageAsync(SearchObject searchObject, CancellationToken cancellationToken)
        {
            var message = await _billService.GetBillsByCompanyIdAsMessageAsync(searchObject, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-bills-by-guest-id/{GuestId}"), Authorize(Roles = "Mobile")]
        public async Task<IActionResult> GetBillsByGuestIdAsMessageAsync(Guid GuestId, CancellationToken cancellationToken)
        {
            var message = await _billService.GetBillsByGuestIdAsMessageAsync(GuestId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpPost("pay-bill"), Authorize(Roles = "Mobile" + "," + "Desktop")]
        public async Task<IActionResult> PayBillAsMessageAsync(BillsPayDto billsPayDto, CancellationToken cancellationToken)
        {
            var message = await _billService.PayBillAsMessageAsync(billsPayDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
