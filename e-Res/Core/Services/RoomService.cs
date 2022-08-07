using AutoMapper;
using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Images;
using Common.Dto.Rooms;
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
    public class RoomService : IRoomService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }

        public RoomService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext=authContext;
        }



        public async Task<Message> CreateRoomAsMessageAsync(RoomCreateDto roomCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var obj = Mapper.Map<Room>(roomCreateDto);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedByUserId = loggedUser.Id;
                obj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.Rooms.AddAsync(obj);
                await _dbContext.SaveChangesAsync();


                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added room",
                    Status = ExceptionCode.Success,
                    Data = roomCreateDto
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

        public async Task<Message> UpdateRoomAsMessageAsync(Guid roomId, RoomUpdateDto roomUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();

                var obj = await _dbContext.Rooms.Where(x => x.Id == roomId).FirstOrDefaultAsync(cancellationToken);
                var realObj = Mapper.Map(roomUpdateDto, obj);
                realObj.ModifiedByUserId = loggedUser.Id;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully updated room",
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

        public async Task<Message> DeleteRoomAsMessageAsync(Guid roomId, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var obj = await _dbContext.Rooms.Where(x => x.Id == roomId).FirstOrDefaultAsync(cancellationToken);
                obj.IsDeleted = true;
                obj.ModifiedByUserId=loggedUser.Id;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully deleted room",
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

        public async Task<Message> GetRoomAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {

                var obj = await _dbContext.Rooms.AsNoTracking().Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);

                if (obj != null)
                {
                    var _obj = Mapper.Map<RoomGetDto>(obj);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got room",
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

        public async Task<Message> GetRoomsByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken)
        {
            try
            {
                var obj =
                    await _dbContext.Rooms
                    .Include(x => x.Company).AsNoTracking()
                    .Where(x => x.CompanyId == baseSearch.Id && !x.IsDeleted && (!(baseSearch.ByName.Length > 0) || x.Title.ToLower().Contains(baseSearch.ByName.ToLower()))).ToListAsync(cancellationToken);

                if (obj != null)
                {
                    var _obj = Mapper.Map<List<RoomGetDto>>(obj);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got rooms",
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
    }
}
