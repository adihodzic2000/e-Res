using AutoMapper;
using Common.Dto;
using Common.Dto.Bills;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Images;
using Common.Dto.ReservationServices;
using Common.Dtos.Bills;
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
    public class BillService : IBillService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }
        public BillService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext = authContext;
        }

        public async Task<Message> CreateBillAsMessageAsync(BillsCreateDto billsCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                if (await _dbContext.Bills.Where(x => x.ReservationId == billsCreateDto.ReservationId && !x.IsDeleted).AnyAsync())
                {
                    return new Message
                    {
                        IsValid = false,
                        Info = "Bill already exists!",
                        Status = ExceptionCode.BadRequest
                    };
                }
                var obj = Mapper.Map<Bill>(billsCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = new Guid("096AF8D7-251B-4BE5-581C-08DA6A7BE48A");
                obj.ModifiedByUserId = new Guid("096AF8D7-251B-4BE5-581C-08DA6A7BE48A");

                await _dbContext.Bills.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added guest",
                    Status = ExceptionCode.Success,
                    Data = billsCreateDto
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

        public async Task<Message> UpdateBillAsMessageAsync(Guid billId, BillsUpdateDto billsUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Bills.Where(x => x.Id == billId).FirstOrDefaultAsync(cancellationToken);
                var realObj = Mapper.Map(billsUpdateDto, obj);
                realObj.ModifiedByUserId = new Guid("096AF8D7-251B-4BE5-581C-08DA6A7BE48A");

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully updated bill",
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

        public async Task<Message> DeleteBillAsMessageAsync(Guid billId, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Bills.Where(x => x.Id == billId).FirstOrDefaultAsync(cancellationToken);
                obj.IsDeleted = true;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully deleted bill",
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

        public async Task<Message> GetBillAsMessageAsync(Guid billId, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Bills.AsNoTracking().Where(x => x.Id == billId && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);

                if (obj != null)
                {
                    var _obj = Mapper.Map<BillsGetDto>(obj);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got bill",
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

        public async Task<Message> GetBillsByCompanyIdAsMessageAsync(SearchObject searchObject, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Bills
                    .Include(x => x.Reservation).Include(x => x.Reservation.Room).Include(x => x.Reservation.Guest)
                    .AsNoTracking()
                    .Where(x =>
                    x.CompanyId == searchObject.Id &&
                    !x.IsDeleted &&
                    (!searchObject.ByName.Any() || (x.Reservation.Guest.FirstName + " " + x.Reservation.Guest.LastName).ToLower().StartsWith(searchObject.ByName.ToLower()) || (x.Reservation.Guest.LastName + " " + x.Reservation.Guest.FirstName).ToLower().StartsWith(searchObject.ByName.ToLower())) &&
                    (x.CreatedDate >= searchObject.DateFrom) &&
                    (x.CreatedDate <= searchObject.DateTo)
                    ).ToListAsync(cancellationToken);


                var _obj = Mapper.Map<List<BillsGetDto>>(obj);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got bills",
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

        public async Task<Message> GetBillsByGuestIdAsMessageAsync(Guid billId, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Bills.Include(x => x.Reservation).Where(x => x.Reservation.GuestId == billId && !x.IsDeleted).ToListAsync(cancellationToken);


                var _obj = Mapper.Map<List<BillsGetDto>>(obj);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got bills",
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

        public async Task<Message> PayBillAsMessageAsync(BillsPayDto billsPayDto, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Bills.Where(x => x.Id == billsPayDto.Id && !x.IsDeleted && !x.IsPaid).FirstOrDefaultAsync(cancellationToken);

                if (obj == null)
                    return new Message
                    {
                        IsValid = false,
                        Info = "Račun nije nađen ili je već plaćen",
                        Status = ExceptionCode.NotFound
                    };
                obj.IsPaid = true;
                obj.PaidDate = DateTime.Now;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Info = "Uspješno plaćanje.",
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

        public async Task<Message> GetBillsByLoggedUserAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var bills = await _dbContext.Bills.Include(x=>x.Company).AsNoTracking().Where(x => x.Reservation.Guest.CreatedByUserId == loggedUser.Id).ToListAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Data=Mapper.Map<List<BillsGetDto>>(bills),
                    Info = "Successfully returned data",
                    Status = ExceptionCode.Success
                };
            }
            catch(Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = ex.Message,
                    Status = ExceptionCode.BadRequest
                };
            }
        }

        public async Task<Message> GetBillDetailsAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var bill = await _dbContext.Bills.Include(x => x.Company).Include(x=>x.Reservation).Include(x=>x.Reservation.Room).AsNoTracking().Where(x => x.Id==Id && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
                var reportData = new ReportDto { 
                    Name="Noćenje",
                    Price=bill.Reservation.Room.Price, 
                    Quantity=(int)((bill.Reservation.DateTo-bill.Reservation.DateFrom).TotalDays), 
                    TotalAmount= (int)((bill.Reservation.DateTo - bill.Reservation.DateFrom).TotalDays)* bill.Reservation.Room.Price 
                };
                var returnList=new List<ReportDto>();
                returnList.Add(reportData);

                var services = await _dbContext.ReservationServices.Include(x => x.Service).Include(x => x.Reservation).Where(x => x.Reservation.Id == bill.ReservationId && !x.IsDeleted).ToListAsync(cancellationToken);

                var _obj = Mapper.Map<List<ReservationServicesGetDto>>(services);

                foreach(var service in _obj)
                {
                    var s = new ReportDto()
                    {
                        Name = service.Service.Title,
                        Price = service.Service.Price,
                        Quantity = service.Quantity,
                        TotalAmount = service.Quantity * service.Service.Price
                    };
                    returnList.Add(s);  
                }

                return new Message
                {
                    IsValid = true,
                    Data = returnList,
                    Info = "Successfully returned data",
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
