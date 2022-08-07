using Common.Dto.Guests;
using Common.Dto.Role;
using Common.Dtos.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IChatService
    {
        Task<Message> CreateMessageAsMessageAsync(CreateMessageDto createMessageDto, CancellationToken cancellationToken);
        Task<Message> GetMessagesAsMessageAsync(Guid primaryUserId, Guid secondaryUserId, CancellationToken cancellationToken);
        Task<Message> GetMyUsersAsMessageAsync(CancellationToken cancellationToken);
        Task<Message> GetUnSeenMessagesAsMessageAsync(CancellationToken cancellationToken);
    }
}
