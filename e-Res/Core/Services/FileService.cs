using AutoMapper;
using Common.Dto;
using Common.Dto.Country;
using Common.Dto.File;
using Common.Dto.Images;
using Common.Helper;
using Core.Interfaces;
using Database;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class FileService : IFileService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }

        public static IWebHostEnvironment _webHostEnvironment;

        public FileService(ERESContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            Mapper = mapper;
            this.authContext=authContext;
        }

        public async Task<Message> UploadImageAsMessageAsync(FileUploadDto fileUploadDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var image = UploadImageHelper.UploadFile(fileUploadDto.ImageURL);
                var file = new ImageCreateDto
                {
                    Path = image,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedByUserId = loggedUser.Id,
                    ModifiedByUserId = loggedUser.Id,
                    UserProfilePictureId= null
            };

                var obj = Mapper.Map<Images>(file);
                await _dbContext.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    Data = Mapper.Map<ImageGetDto>(obj),
                    IsValid = true,
                    Info = "Success",
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = "Bad Request",
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> GetImagesByCompanyAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var logo = await _dbContext.Companies.Include(x=>x.Logo).Where(x => x.Id == loggedUser.CompanyId).FirstOrDefaultAsync(cancellationToken);
                var images = await _dbContext.Images.Include(x => x.CreatedByUser).Where(x => x.CreatedByUser.CompanyId == loggedUser.CompanyId && x.Id!=logo.LogoId && !x.IsDeleted && x.UserProfilePicture==null).ToListAsync(cancellationToken);

                return new Message
                {
                    Data = Mapper.Map<List<ImageGetDto>>(images),
                    IsValid = true,
                    Info = "Success",
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = "Bad Request",
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> DeleteImageByCompanyAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var logo = await _dbContext.Images.Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
                logo.IsDeleted = true;
                logo.ModifiedByUserId = loggedUser.Id;
                _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    Data = Id,
                    IsValid = true,
                    Info = "Successfully deleted",
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = "Bad Request",
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> GetImagesByCompanyIdAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var images = await _dbContext.Images.Include(x => x.CreatedByUser).Where(x => x.CreatedByUser.CompanyId == Id && !x.IsDeleted && x.UserProfilePicture==null).ToListAsync(cancellationToken);

                return new Message
                {
                    Data = Mapper.Map<List<ImageGetDto>>(images),
                    IsValid = true,
                    Info = "Success",
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = "Bad Request",
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> ChangeMyProfilePictureAsMessageAsync(FileUploadDto fileUploadDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var image = UploadImageHelper.UploadFile(fileUploadDto.ImageURL);
                var file = new ImageCreateDto
                {
                    Path = image,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedByUserId = loggedUser.Id,
                    ModifiedByUserId = loggedUser.Id,
                    UserProfilePictureId=loggedUser.Id
                };
                var images = await _dbContext.Images.Where(x => x.UserProfilePictureId == loggedUser.Id && !x.IsDeleted).ToListAsync(cancellationToken);
                foreach (var im in images)
                    im.IsDeleted = true;

                var obj = Mapper.Map<Images>(file);
                await _dbContext.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    Data = Mapper.Map<ImageGetDto>(obj),
                    IsValid = true,
                    Info = "Success",
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = "Bad Request",
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> GetProfilePictureAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var image = await _dbContext.Images.Where(x => x.UserProfilePictureId== loggedUser.Id && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);

                return new Message
                {
                    Data = Mapper.Map<ImageGetDto>(image),
                    IsValid = true,
                    Info = "Success",
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = "Bad Request",
                    Status = ExceptionCode.BadRequest
                };
            }
        }
    }
}
