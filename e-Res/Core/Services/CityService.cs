using AutoMapper;
using Common.Dto;
using Common.Dto.City;
using Common.Dto.Company;
using Common.Dto.Country;
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
    public class CityService : ICityService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }


        public CityService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext=authContext;
        }

        public async Task<Message> CreateCityAsMessageAsync(CityCreateDto cityCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var obj = Mapper.Map<City>(cityCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.Cities.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added city",
                    Status = ExceptionCode.Success,
                    Data = obj
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

        public async Task<Message> GetCityAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var city = await _dbContext.Cities.Include(x=>x.Country).AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);

                if (city != null)
                {
                    var obj = Mapper.Map<CityGetDto>(city);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got city",
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

        public async Task<Message> GetCitesByCountryIdAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var city = await _dbContext.Cities.Include(x => x.Country).AsNoTracking().Where(x => x.CountryId == Id).ToListAsync(cancellationToken);

                if (city != null)
                {
                    var obj = Mapper.Map<List<CityGetDto>>(city);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got cities",
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
    }
}
