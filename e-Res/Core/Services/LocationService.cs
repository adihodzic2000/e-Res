using AutoMapper;
using Common.Dto;
using Common.Dto.City;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Images;
using Common.Dto.Locations;
using Common.Helper;
using Core.Interfaces;
using Database;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class LocationService : ILocationService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }

        public LocationService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext = authContext;
        }

        public async Task<Message> CreateLocationAsMessageAsync(LocationCreateDto locationCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var obj = Mapper.Map<Location>(locationCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.Locations.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added location",
                    Status = ExceptionCode.Success,
                    Data = Mapper.Map<LocationGetDto>(obj)
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = ex.Message,
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> GetLocationAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var location = await _dbContext.Locations.Include(x=>x.City).ThenInclude(x=>x.Country).AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);

                if (location != null)
                {
                    var obj = Mapper.Map<LocationGetDto>(location);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got location",
                        Status = ExceptionCode.Success,
                        Data = obj
                    };
                }
                return new Message
                {
                    IsValid = true,
                    Status = ExceptionCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = ex.Message,
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> DeleteLocationAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Locations.Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
                obj.IsDeleted = true;
                obj.ModifiedByUserId = loggedUser.Id;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully deleted location",
                    Status = ExceptionCode.Success,
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = ex.Message,
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public  async Task<Message> UpdateLocationAsMessageAsync(Guid Id, LocationUpdateDto locationUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Locations.Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
                var realObj = Mapper.Map(locationUpdateDto, obj);
                realObj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully updated location",
                    Status = ExceptionCode.Success,
                };
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = ex.Message,
                    Status = ExceptionCode.BadRequest
                };
            }
        }
    }
}
