using AutoMapper;
using Common.Dto;
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
    public class CountryService : ICountryService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }


        public CountryService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext = authContext;
        }


        public async Task<Message> CreateCountryAsMessageAsync(CountryCreateDto countryCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var obj = Mapper.Map<Country>(countryCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.Countries.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added country",
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

        public async Task<Message> GetCountryAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var country = await _dbContext.Countries.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);

                if (country != null)
                {
                    var obj = Mapper.Map<CountryGetDto>(country);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got country",
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

        public async Task<Message> GetCountriesAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var countries = await _dbContext.Countries.AsNoTracking().ToListAsync(cancellationToken);


                var obj = Mapper.Map<List<CountryGetDto>>(countries);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully returned countries",
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
    }
}
