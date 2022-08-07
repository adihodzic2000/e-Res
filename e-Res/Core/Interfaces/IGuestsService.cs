using Common.Dto.Guests;
using Common.Dto.Role;
using Core.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGuestsService
    {
        Task<Message> CreateGuestAsMessageAsync(GuestCreateDto guestCreateDto, CancellationToken cancellationToken);
        Task<Message> UpdateGuestAsMessageAsync(Guid guestId, GuestUpdateDto guestUpdateDto, CancellationToken cancellationToken);
        Task<Message> DeleteGuestAsMessageAsync(Guid guestId, CancellationToken cancellationToken);
        Task<Message> GetGuestAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> GetGuestByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken);
    }
}
