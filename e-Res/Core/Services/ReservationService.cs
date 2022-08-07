using AutoMapper;
using Common.Dto;
using Common.Dto.Bills;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Images;
using Common.Dto.Reservations;
using Common.Dto.ReservationServices;
using Common.Dto.Rooms;
using Common.Dtos.Reservations;
using Common.Dtos.Reviews;
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
    public class ReservationService : IReservationService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        public IBillService BillService { get; set; }
        private IAuthContext authContext { get; set; }

        public ReservationService(ERESContext dbContext, IMapper mapper, IBillService billService, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            BillService = billService;
            this.authContext = authContext;
        }



        private bool ReservationExistInDatabase(Guid roomId, DateTime dateFrom, DateTime dateTo)
        {
            return _dbContext.Reservations
                .AsNoTracking()
                .Where(x => !(dateFrom >= x.DateTo || dateTo <= x.DateFrom) && (x.RoomId == roomId && !x.IsDeleted))//nepotrebno provjeravanje companyId-a
                .Count() > 0;
        }

        public async Task<Message> CreateReservationAsMessageAsync(ReservationCreateDto reservationCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                if (reservationCreateDto.RoomId != null)
                {
                    if(reservationCreateDto.DateTo<=reservationCreateDto.DateFrom)
                        return new Message
                        {
                            IsValid = false,
                            Info = "Bad dates!",
                            Status = ExceptionCode.BadRequest
                        };
                    bool exist = ReservationExistInDatabase((Guid)reservationCreateDto.RoomId, reservationCreateDto.DateFrom, reservationCreateDto.DateTo);
                    if (exist)
                    {
                        return new Message
                        {
                            IsValid = false,
                            Info = "Room is not opened in that period!",
                            Status = ExceptionCode.BadRequest
                        };
                    }
                }
                if(reservationCreateDto.RoomId==Guid.Empty)
                foreach (var room in await _dbContext.Rooms.Where(x=>!x.IsDeleted && loggedUser.CompanyId==x.CompanyId).ToListAsync(cancellationToken))
                {
                    if (!ReservationExistInDatabase(room.Id, reservationCreateDto.DateFrom, reservationCreateDto.DateTo))
                    {
                        reservationCreateDto.RoomId = room.Id;
                        break;
                    }
                }
                if (reservationCreateDto.RoomId == new Guid("00000000-0000-0000-0000-000000000000") || reservationCreateDto.RoomId == null)
                    return new Message
                    {
                        IsValid = false,
                        Info = "No rooms that are opened in that period!",
                        Status = ExceptionCode.BadRequest
                    };

                var _room = await _dbContext.Rooms.FirstOrDefaultAsync(x => x.Id == reservationCreateDto.RoomId);


                var obj = Mapper.Map<Reservation>(reservationCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;
                obj.TotalAmount = (reservationCreateDto.DateTo - reservationCreateDto.DateFrom).TotalDays * _room.Price;
                await _dbContext.Reservations.AddAsync(obj);
                await _dbContext.SaveChangesAsync();


                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added reservation",
                    Status = ExceptionCode.Success,
                    Data = reservationCreateDto
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

        public async Task<Message> UpdateReservationAsMessageAsync(Guid reservationId, ReservationUpdateDto reservationUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Reservations.Where(x => x.Id == reservationId && !x.IsDeleted && !x.IsFinished).FirstOrDefaultAsync(cancellationToken);
                var realObj = Mapper.Map(reservationUpdateDto, obj);
                realObj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully updated reservation",
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

        public async Task<Message> DeleteReservationAsMessageAsync(Guid reservationId, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Reservations.Where(x => x.Id == reservationId).FirstOrDefaultAsync(cancellationToken);
                obj.IsDeleted = true;
                obj.ModifiedByUserId = loggedUser.Id;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully deleted reservation",
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

        public async Task<Message> GetReservationAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Reservations.Include(x=>x.Room).Include(x=>x.Guest).AsNoTracking().Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);


                var _obj = Mapper.Map<ReservationGetDto>(obj);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got reservation",
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

        public async Task<Message> GetReservationsByCompanyIdAsMessageAsync( CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var listOfReservations = await _dbContext.Rooms.Where(x => x.CompanyId == loggedUser.CompanyId && !x.IsDeleted).ToListAsync(cancellationToken);
                var obj = await _dbContext.Reservations.Include(x => x.Guest).Include(x => x.Room).AsNoTracking()
                    .Where(x => listOfReservations.Contains(x.Room) && !x.IsDeleted).ToListAsync(cancellationToken);


                var _obj = Mapper.Map<List<ReservationGetDto>>(obj);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got reservations",
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

        public async Task<Message> GetTopXReservationsByCompanyIdAsMessageAsync(int numberOfReservations, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var listOfReservations = await _dbContext.Rooms.Where(x => x.CompanyId == loggedUser.CompanyId && !x.IsDeleted).ToListAsync(cancellationToken);
                var obj = await _dbContext.Reservations.Include(x => x.Guest).Include(x => x.Room).AsNoTracking()
                    .Where(x => listOfReservations.Contains(x.Room) && x.DateFrom >= DateTime.Now && !x.IsDeleted).OrderBy(x => x.DateFrom).Take(numberOfReservations).ToListAsync(cancellationToken);


                var _obj = Mapper.Map<List<ReservationGetDto>>(obj);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got reservations",
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

        public async Task<Message> MarkReservationAsFinishedAsMessageAsync(FinishReservationDto finishReservationDto, CancellationToken cancellationToken)
        {
            try
            {

                var obj = await _dbContext.Reservations.Include(x => x.Room)
                    .Where(x => !x.IsFinished && !x.IsDeleted && finishReservationDto.Id == x.Id).FirstOrDefaultAsync(cancellationToken);
                if (obj == null)
                    return new Message
                    {
                        IsValid = false,
                        Info = "Reservation doesn't exist",
                        Status = ExceptionCode.BadRequest
                    };

                var reservationServices = await _dbContext.ReservationServices.Include(x => x.Service).Where(x => x.ReservationId == obj.Id).ToListAsync();
                double sum = 0;
                foreach (var rServices in reservationServices)
                {
                    sum += rServices.Service.Price*rServices.Quantity;
                }

                Mapper.Map(finishReservationDto, obj);

                BillsCreateDto billsCreateDto = new BillsCreateDto();
                billsCreateDto.PriceOfNight = obj.Room.Price;
                billsCreateDto.ReservationId = obj.Id;
                billsCreateDto.TotalAmountOfServices = sum;
                billsCreateDto.TotalAmountOfNights = (obj.DateTo - obj.DateFrom).TotalDays * obj.Room.Price;
                billsCreateDto.CompanyId = obj.Room.CompanyId;
                billsCreateDto.TotalAmount = sum + (obj.DateTo - obj.DateFrom).TotalDays * obj.Room.Price;

                await BillService.CreateBillAsMessageAsync(billsCreateDto, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully finished reservation",
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

        public async Task<Message> GetRevenueOfReservationsByLastMonth(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var lastMonth = DateTime.Now.AddMonths(-1).Month;

                var sum = await _dbContext.Bills.AsNoTracking().Where(x => loggedUser.CompanyId == x.CompanyId && (x.PaidDate.HasValue && ((DateTime)x.PaidDate).Month == lastMonth)).Select(x => x.TotalAmount).SumAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got amount",
                    Status = ExceptionCode.Success,
                    Data = sum
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

        public async Task<Message> GetRevenueOfReservationsByCurrentMonth(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var lastMonth = DateTime.Now.Month;

                var sum = await _dbContext.Bills.AsNoTracking().Where(x => loggedUser.CompanyId == x.CompanyId && (x.PaidDate.HasValue && ((DateTime)x.PaidDate).Month == lastMonth)).Select(x => x.TotalAmount).SumAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got amount",
                    Status = ExceptionCode.Success,
                    Data = sum
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

        public async Task<Message> GetNumberOfReservationsByCurrentMonth(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var currentMonth = DateTime.Now.Month;

                var counter = await _dbContext.Reservations.Include(x=>x.Room).AsNoTracking().Where(x => loggedUser.CompanyId == x.Room.CompanyId && x.DateFrom.Month==currentMonth).CountAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got number of reservations",
                    Status = ExceptionCode.Success,
                    Data = counter
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

        public async Task<Message> GetReservationServicesAsMessageAsync(Guid ReservationId, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var services= await _dbContext.ReservationServices.Include(x=>x.Service).Include(x=>x.Reservation).Where(x => x.Reservation.Id== ReservationId  && !x.IsDeleted).ToListAsync(cancellationToken);
      


                var _obj = Mapper.Map<List<ReservationServicesGetDto>>(services);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got reservation services",
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

        public async Task<Message> AddReviewToCompanyAsMessageAsync(ReviewCreateDto reviewCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var obj = Mapper.Map<Reviews>(reviewCreateDto);
                await _dbContext.Reviews.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message { Info = "Successfuly returned data", IsValid=false,Status = ExceptionCode.Success, Data = Mapper.Map<ReviewGetDto>(obj) };
            }
            catch(Exception ex)
            {
                return new Message { Info = "Error while adding data", IsValid=false, Status = ExceptionCode.BadRequest};
            }
        }
    }
}
