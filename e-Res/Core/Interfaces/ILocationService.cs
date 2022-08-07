using Common.Dto;
using Common.Dto.City;
using Common.Dto.Country;
using Common.Dto.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILocationService
    {
        Task<Message> CreateLocationAsMessageAsync(LocationCreateDto locationCreateDto, CancellationToken cancellationToken);
        Task<Message> GetLocationAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> DeleteLocationAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> UpdateLocationAsMessageAsync(Guid Id,LocationUpdateDto locationUpdateDto, CancellationToken cancellationToken);

    }
}
