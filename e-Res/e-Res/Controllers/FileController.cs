using Common.Dto;
using Common.Dto.File;
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
    public class FileController : ControllerBase
    {

        private readonly IFileService _fileService;

        public FileController(IFileService _fileService)
        {
            this._fileService = _fileService;
        }

        //[HttpPost, AllowAnonymous]
        //public async Task<IActionResult> UploadFileAsMessageDto([FromForm]FileUploadDto fileUploadDto, CancellationToken cancellationToken)
        //{
        //    var message = await _fileService.UploadImageAsMessage(fileUploadDto,cancellationToken);
        //    if (message.IsValid == false)
        //        return BadRequest(message);
        //    return Ok(message);
        //}
        [HttpPost("upload-image"), Authorize()]
        public async Task<IActionResult> UploadFileAsMessage(FileUploadDto fileUploadDto, CancellationToken cancellationToken)
        {
            var message = await _fileService.UploadImageAsMessageAsync(fileUploadDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpPost("upload-profile-image"), Authorize()]
        public async Task<IActionResult> UploadProfileFileAsMessage(FileUploadDto fileUploadDto, CancellationToken cancellationToken)
        {
            var message = await _fileService.ChangeMyProfilePictureAsMessageAsync(fileUploadDto, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-images"), Authorize()]
        public async Task<IActionResult> GetFilesByCompany(CancellationToken cancellationToken)
        {
            var message = await _fileService.GetImagesByCompanyAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-profile-image"), Authorize()]
        public async Task<IActionResult> GetProfileImageAsMessageAsync(CancellationToken cancellationToken)
        {
            var message = await _fileService.GetProfilePictureAsMessageAsync(cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpGet("get-images/{CompanyId}"), Authorize()]
        public async Task<IActionResult> GetFilesByCompanyId(Guid CompanyId, CancellationToken cancellationToken)
        {
            var message = await _fileService.GetImagesByCompanyIdAsMessageAsync(CompanyId, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
        [HttpDelete("delete-image/{Id}"), Authorize()]
        public async Task<IActionResult> DeleteImageAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            var message = await _fileService.DeleteImageByCompanyAsMessageAsync(Id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
