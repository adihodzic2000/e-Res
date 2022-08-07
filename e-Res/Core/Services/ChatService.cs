using AutoMapper;
using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Images;
using Common.Dto.File;
using Common.Dto.Country;
using Common.Helper;
using Core.Interfaces;
using Database;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Common.Dto.Guests;
using Common.Dtos.Chat;
using Common.Dto.User;

namespace Core.Services
{
    public class ChatService : IChatService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }


        public ChatService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext = authContext;
        }

        public async Task<Message> CreateMessageAsMessageAsync(CreateMessageDto createMessageDto, CancellationToken cancellationToken)
        {
            try
            {
                var checkedUser = await _dbContext.Users.Where(x => x.Id == createMessageDto.UserToId).FirstOrDefaultAsync(cancellationToken);
                if (checkedUser == null)
                {
                    checkedUser = await _dbContext.Users.Where(x => x.CompanyId == createMessageDto.UserToId && !x.IsDeleted).FirstOrDefaultAsync();
                }
                if (checkedUser == null)
                {
                    return new Message
                    {
                        IsValid = false,
                        Info = "User doesn't exist in database!",
                        Status = ExceptionCode.BadRequest
                    };
                }
                createMessageDto.UserToId = checkedUser.Id;

                var obj = Mapper.Map<Chat>(createMessageDto);
                obj.CreatedDate = DateTime.Now;

                await _dbContext.Chat.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added message",
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

        public async Task<Message> GetMessagesAsMessageAsync(Guid primaryUserId, Guid secondaryUserId, CancellationToken cancellationToken)
        {
            try
            {
                var chat = await _dbContext.Chat
                    .Include(x => x.UserFrom)
                    .Include(x => x.UserTo)
                    .AsNoTracking()
                    .Where(x => (x.UserFromId == primaryUserId && x.UserToId == secondaryUserId) || x.UserFromId == secondaryUserId && x.UserToId == primaryUserId)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToListAsync(cancellationToken);

                if (chat != null)
                {
                    var obj = Mapper.Map<List<GetMessageDto>>(chat);
                    return new Message
                    {
                        IsValid = true,
                        Info = "Successfully got messages",
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

        public async Task<Message> GetMyUsersAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var chats = await _dbContext.Chat.Include(x => x.UserFrom).Include(x => x.UserTo).AsNoTracking().Where(x => x.UserFromId == loggedUser.Id || x.UserToId == loggedUser.Id).ToListAsync(cancellationToken);

                List<User> users = new List<User>();
                foreach (var chat in chats)
                {
                    var user = new User();
                    if (chat.UserFromId != loggedUser.Id)
                    {
                        user = chat.UserFrom as User;
                    }
                    else if (chat.UserToId != loggedUser.Id)
                    {
                        user = chat.UserTo as User;
                    }
                    else user = null;
                    bool found = false;
                    foreach (var n in users)
                    {
                        if (n.Id == user.Id)
                            found = true;
                    }
                    if (!found)
                        users.Add(user);
                }
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got users",
                    Status = ExceptionCode.Success,
                    Data = Mapper.Map<List<UserGetDto>>(users)
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

      

        public async Task<Message> GetUnSeenMessagesAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var chats = await _dbContext.Chat.Where(x => x.UserToId == loggedUser.Id && !x.Seen && !x.IsDeleted).ToListAsync(cancellationToken);

                foreach (var chat in chats)
                {
                    chat.Seen = true;
                }
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got messages",
                    Status = ExceptionCode.Success,
                    Data = Mapper.Map<List<GetMessageDto>>(chats)
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
