using Common.Dto;
using Common.Dto.Country;
using Common.Dtos.Chat;
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
    public class ChatController : ControllerBase
    {

        private readonly IChatService _chatService;

        public ChatController(IChatService _chatService)
        {
            this._chatService = _chatService;
        }


        [HttpPost("create-message"), Authorize()]
        public async Task<IActionResult> CreateMessageAsMessageAsync(CreateMessageDto createMessageDto, CancellationToken cancellationToken)
        {
            var message = await _chatService.CreateMessageAsMessageAsync(createMessageDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-messages/{primaryUserId}/{secondaryUserId}"), Authorize()]
        public async Task<IActionResult> GetMessagesAsMessageAsync(Guid primaryUserId, Guid secondaryUserId, CancellationToken cancellationToken)
        {
            var message = await _chatService.GetMessagesAsMessageAsync(primaryUserId, secondaryUserId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-unseen-messages"), Authorize()]
        public async Task<IActionResult> GetUnSeenMessagesAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _chatService.GetUnSeenMessagesAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-my-users"), Authorize()]
        public async Task<IActionResult> GetMyUsersAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _chatService.GetMyUsersAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
