using Common.Dto.Guests;
using Common.Dto.Role;
using Common.Dtos.Chat;
using Core.SearchObjects;
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
        Task<Message> GetMyUsersAsMessageAsync(SearchByName search, CancellationToken cancellationToken);
        Task<Message> GetUnSeenMessagesAsMessageAsync(CancellationToken cancellationToken);
        Task<Message> GetUnClickedMessagesAsMessageAsync(CancellationToken cancellationToken);
        Task<Message> SeeUnClickedMessagesAsMessageAsync(Guid Id, CancellationToken cancellationToken);
    }
}
