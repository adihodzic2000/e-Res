using Common.Dto.Guests;
using Common.Dto.Role;
using Common.Dto.Rooms;
using Core.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoomService
    {
        Task<Message> CreateRoomAsMessageAsync(RoomCreateDto roomCreateDto, CancellationToken cancellationToken);
        Task<Message> UpdateRoomAsMessageAsync(Guid roomId, RoomUpdateDto roomUpdateDto, CancellationToken cancellationToken);
        Task<Message> DeleteRoomAsMessageAsync(Guid roomId, CancellationToken cancellationToken);
        Task<Message> GetRoomAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> GetRoomsByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken);
    }
}
