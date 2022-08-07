using AutoMapper;
using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Images;
using Common.Helper;
using Core.Interfaces;
using Core.SearchObjects;
using Database;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class GuestsService : IGuestsService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }

        public GuestsService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext = authContext;
        }

        public async Task<Message> CreateGuestAsMessageAsync(GuestCreateDto guestCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var obj = Mapper.Map<Guest>(guestCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.Guests.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added guest",
                    Status = ExceptionCode.Success,
                    Data = obj.Id
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

        public async Task<Message> UpdateGuestAsMessageAsync(Guid guestId, GuestUpdateDto guestUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Guests.Where(x => x.Id == guestId).FirstOrDefaultAsync(cancellationToken);
                var realObj = Mapper.Map(guestUpdateDto, obj);
                realObj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully updated guest",
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

        public async Task<Message> DeleteGuestAsMessageAsync(Guid guestId, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Guests.Where(x => x.Id == guestId && loggedUser.CompanyId==x.CompanyId).FirstOrDefaultAsync(cancellationToken);

                if (obj != null)
                {

                obj.IsDeleted = true;
                obj.ModifiedByUserId= loggedUser.Id;
                await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully deleted guest",
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

        public async Task<Message> GetGuestAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Guests.AsNoTracking().Where(x => x.Id == Id && !x.IsDeleted && loggedUser.CompanyId == x.CompanyId).FirstOrDefaultAsync(cancellationToken);

                if (obj != null)
                {
                    var _obj = Mapper.Map<GuestGetDto>(obj);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got guest",
                        Status = ExceptionCode.Success,
                        Data = _obj
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

        public async Task<Message> GetGuestByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken)
        {
            try
            {

                var obj = await _dbContext.Guests.AsNoTracking().Where(x => x.CompanyId == baseSearch.Id && !x.IsDeleted && (!(baseSearch.ByName.Length > 0) || (x.FirstName + " " + x.LastName).ToLower().Contains(baseSearch.ByName.ToLower()) || (x.LastName + " " + x.FirstName).ToLower().Contains(baseSearch.ByName.ToLower()))).ToListAsync(cancellationToken);


                var _obj = Mapper.Map<List<GuestGetDto>>(obj);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got guests",
                    Status = ExceptionCode.Success,
                    Data = _obj
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
