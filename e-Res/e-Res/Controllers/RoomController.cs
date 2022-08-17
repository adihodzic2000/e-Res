using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Rooms;
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
    public class RoomController : ControllerBase
    {

        private readonly IRoomService _roomsService;

        public RoomController(IRoomService _roomsService)
        {
            this._roomsService = _roomsService;
        }


        [HttpPost("add-room"), Authorize(Roles="Desktop")]
        public async Task<IActionResult> AddRoomAsMessageAsync(RoomCreateDto roomCreateDto, CancellationToken cancellationToken)
        {
            var message = await _roomsService.CreateRoomAsMessageAsync(roomCreateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("get-room/{Id}"), Authorize()]
        public async Task<IActionResult> GetRoomAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _roomsService.GetRoomAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpPut("update-room/{Id}"), Authorize(Roles = "Desktop")]
        public async Task<IActionResult> UpdateRoomAsMessageAsync(Guid Id, RoomUpdateDto roomUpdateDto, CancellationToken cancellationToken)
        {
            var message = await _roomsService.UpdateRoomAsMessageAsync(Id, roomUpdateDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpDelete("delete-room/{Id}"), Authorize(Roles = "Desktop")]
        public async Task<IActionResult> DeleteRoomAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _roomsService.DeleteRoomAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpPost("get-rooms-by-company-id"), Authorize()]
        public async Task<IActionResult> GetGuestByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken)
        {
            var message = await _roomsService.GetRoomsByCompanyIdAsMessageAsync(baseSearch, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
