using Common.Dto;
using Common.Dto.City;
using Common.Dto.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICityService
    {
        Task<Message> CreateCityAsMessageAsync(CityCreateDto cityCreateDto, CancellationToken cancellationToken);
        Task<Message> GetCityAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> GetCitesByCountryIdAsMessageAsync(Guid Id, CancellationToken cancellationToken);

    }
}
