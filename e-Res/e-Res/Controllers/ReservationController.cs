using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Reservations;
using Common.Dtos.Reservations;
using Common.Dtos.Reviews;
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
    public class ReservationController : ControllerBase
    {

        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService _reservationService)
        {
            this._reservationService = _reservationService;
        }


        [HttpPost("create-reservation"), Authorize]
        public async Task<IActionResult> CreateReservationAsMessageAsync(ReservationCreateDto reservationCreateDto, CancellationToken cancellationToken)
        {
            var message = await _reservationService.CreateReservationAsMessageAsync(reservationCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-reservation/{Id}"), Authorize]
        public async Task<IActionResult> GetReservationAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _reservationService.GetReservationAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-reservation-services/{ReservationId}"), Authorize]
        public async Task<IActionResult> GetReservationServicesAsMessageAsync(Guid ReservationId, CancellationToken cancellationToken)
        {
            var message = await _reservationService.GetReservationServicesAsMessageAsync(ReservationId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPut("update-reservation/{Id}"), Authorize]
        public async Task<IActionResult> UpdateReservationAsMessageAsync(Guid Id, ReservationUpdateDto reservationUpdateDto, CancellationToken cancellationToken)
        {
            var message = await _reservationService.UpdateReservationAsMessageAsync(Id, reservationUpdateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpDelete("delete-reservation/{Id}"), Authorize]
        public async Task<IActionResult> DeleteReservationAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _reservationService.DeleteReservationAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-reservations-by-company-id"), Authorize]
        public async Task<IActionResult> GetReservationsByCompanyIdAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _reservationService.GetReservationsByCompanyIdAsMessageAsync( cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-x-reservations-by-company-id/{numberOfReservations}"), Authorize]
        public async Task<IActionResult> GetXReservationsByCompanyIdAsMessageAsync(int numberOfReservations, CancellationToken cancellationToken)
        {
            var message = await _reservationService.GetTopXReservationsByCompanyIdAsMessageAsync(numberOfReservations, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }  
        [HttpGet("get-revenue-of-reservations-by-last-month"), Authorize]
        public async Task<IActionResult> GetRevenueOfReservationsByLastMonthAsMessageAsync( CancellationToken cancellationToken)
        {
            var message = await _reservationService.GetRevenueOfReservationsByLastMonth( cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-revenue-of-reservations-by-current-month"), Authorize]
        public async Task<IActionResult> GetRevenueOfReservationsByCurrentMonthAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _reservationService.GetRevenueOfReservationsByCurrentMonth(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPost("mark-reservation-as-finished"), Authorize]
        public async Task<IActionResult> MarkReservationAsMessageAsync(FinishReservationDto finishReservationDto, CancellationToken cancellationToken)
        {
            var message = await _reservationService.MarkReservationAsFinishedAsMessageAsync(finishReservationDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-number-of-reservations-by-current-month"), Authorize]
        public async Task<IActionResult> GetNumberOfReservationsByCurrentMonthAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _reservationService.GetNumberOfReservationsByCurrentMonth(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPost("add-review"), Authorize(Roles ="Mobile")]
        public async Task<IActionResult> AddReviewToCompanyAsMessageAsync(ReviewCreateDto reviewCreateDto,CancellationToken cancellationToken)
        {
            var message = await _reservationService.AddReviewToCompanyAsMessageAsync(reviewCreateDto,cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
