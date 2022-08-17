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
using Core.SearchObjects;
using System.Net.Mail;
using System.Text;

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
        public bool _SendMail(string To, string Subject, string Sadrzaj)
        {
            MailMessage message = new MailMessage("mverifikacija@gmail.com", To);
            message.Subject = Subject;
            message.Body = Sadrzaj;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("mverifikacija@gmail.com", "kvajdweznozeynay");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Message> CreateMessageAsMessageAsync(CreateMessageDto createMessageDto, CancellationToken cancellationToken)
        {
            try
            {
                var userFrom = await _dbContext.Users.Where(x => x.Id == createMessageDto.UserFromId && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
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
                bool mailValid = _SendMail(checkedUser.Email, "Imate novu poruku od korisnika: " + userFrom.FirstName + " " + userFrom.LastName, createMessageDto.Content);

                var obj = Mapper.Map<Chat>(createMessageDto);
                obj.CreatedDate = DateTime.Now;

                await _dbContext.Chat.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added message",
                    Status = ExceptionCode.Success,
                    Data = null
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

                var realObjs = Mapper.Map<List<GetMessageDto>>(chat);
                var image = await _dbContext.Images.Where(x => x.UserProfilePictureId == realObjs[0].UserFromId && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
                var mappedImage = Mapper.Map<ImageGetDto>(image);
                foreach (var obj in realObjs)
                {
                    obj.UserFrom.Image = mappedImage;
                }

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got messages",
                    Status = ExceptionCode.Success,
                    Data = realObjs
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

        public async Task<Message> GetMyUsersAsMessageAsync(SearchByName search, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var chats = await _dbContext.Chat
                    .Include(x => x.UserFrom)
                    .Include(x => x.UserTo)
                    .Include(x => x.UserTo.Company)
                    .AsNoTracking()
                    .Where(x =>
                    ((x.UserFrom.IsDeleted == false &&
                     x.UserFromId == loggedUser.Id)

                    ||

                    (x.UserTo.IsDeleted == false

                    && x.UserToId == loggedUser.Id))

                    && ((((x.UserFrom.FirstName + " " + x.UserFrom.LastName).ToLower().Contains(search.Name.ToLower())) ||
                    ((x.UserFrom.LastName + " " + x.UserFrom.FirstName).ToLower().Contains(search.Name.ToLower())) || (((x.UserTo.FirstName + " " + x.UserTo.LastName).ToLower().Contains(search.Name.ToLower())) ||
                    ((x.UserTo.LastName + " " + x.UserTo.FirstName).ToLower().Contains(search.Name.ToLower())))))).ToListAsync(cancellationToken);

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
                var realObjs = Mapper.Map<List<UserGetDto>>(users);
                foreach (var user in realObjs)
                {
                    user.Image = Mapper.Map<ImageGetDto>(await _dbContext.Images.Where(x => x.UserProfilePictureId == user.Id && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken));
                }

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got users",
                    Status = ExceptionCode.Success,
                    Data = realObjs
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
                var chats = await _dbContext.Chat.Include(x => x.UserFrom)
                    .Include(x => x.UserTo).Where(x => x.UserToId == loggedUser.Id && !x.Seen && !x.IsDeleted).ToListAsync(cancellationToken);

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

        public async Task<Message> GetUnClickedMessagesAsMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var chats = await _dbContext.Chat.Include(x => x.UserFrom)
                    .Include(x => x.UserTo).Where(x => x.UserToId == loggedUser.Id && !x.Clicked && !x.IsDeleted).ToListAsync(cancellationToken);

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

        public async Task<Message> SeeUnClickedMessagesAsMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await authContext.GetLoggedUser();
                var chat = await _dbContext.Chat.Where(x => x.UserToId == loggedUser.Id && x.UserFromId == Id && !x.Clicked && !x.IsDeleted).ToListAsync(cancellationToken);
                foreach (var message in chat)
                {
                    message.Clicked = true;
                }
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Message
                {
                    IsValid = true,
                    Info = "Done!",
                    Status = ExceptionCode.Success,
                    Data = Mapper.Map<List<GetMessageDto>>(chat)
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
