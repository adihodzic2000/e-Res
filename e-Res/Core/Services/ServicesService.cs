using AutoMapper;
using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Images;
using Common.Dtos.Services;
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
    public class ServicesService : IServicesService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }
        public ServicesService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext = authContext;
        }



        public async Task<Message> CreateServiceAsMessageAsync(ServicesCreateDto serviceCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var obj = Mapper.Map<Service>(serviceCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.Services.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added service",
                    Status = ExceptionCode.Success,
                    Data = serviceCreateDto
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

        public async Task<Message> UpdateServiceAsMessageAsync(Guid serviceId, ServicesUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Services.Where(x => x.Id == serviceId).FirstOrDefaultAsync(cancellationToken);
                var realObj = Mapper.Map(serviceUpdateDto, obj);
                realObj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully updated service",
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

        public async Task<Message> DeleteServiceAsMessageAsync(Guid serviceId, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Services.Where(x => x.Id == serviceId).FirstOrDefaultAsync(cancellationToken);
                obj.IsDeleted = true;
                obj.ModifiedByUserId = loggedUser.Id;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully deleted service",
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

        public async Task<Message> GetServiceAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Services.AsNoTracking().Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);

                if (obj != null)
                {
                    var _obj = Mapper.Map<ServicesGetDto>(obj);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got service",
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

        public async Task<Message> GetServicesByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await _dbContext.Users.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == new Guid("096AF8D7-251B-4BE5-581C-08DA6A7BE48A"));

                var obj = await _dbContext.Services
                    .Include(x => x.CreatedByUser)
                    .AsNoTracking()
                    .Where(x =>
                        x.CreatedByUser.CompanyId == baseSearch.Id && !x.IsDeleted &&
                        (!(baseSearch.ByName.Length > 0) || x.Title.ToLower().Contains(baseSearch.ByName.ToLower()))).ToListAsync(cancellationToken);

                if (obj != null)
                {
                    var _obj = Mapper.Map<List<ServicesGetDto>>(obj);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got services",
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

        public async Task<Message> AddServiceToGuestIdAsMessageAsync(AddServiceToGuest addServiceToGuest, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();


                var obj = Mapper.Map<Entities.ReservationServices>(addServiceToGuest);
                await _dbContext.ReservationServices.AddAsync(obj);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;

                var service = await _dbContext.Services.AsNoTracking().FirstOrDefaultAsync(x => x.Id == addServiceToGuest.ServiceId);
                var reservation = await _dbContext.Reservations.Include(x => x.Room).Where(x => x.Id == addServiceToGuest.ReservationId).FirstOrDefaultAsync(cancellationToken);
                reservation.TotalAmount += addServiceToGuest.Quantity*service.Price;

                await _dbContext.SaveChangesAsync();

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added service to user",
                    Status = ExceptionCode.Success,
                    Data = addServiceToGuest
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
