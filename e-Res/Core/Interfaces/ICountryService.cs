using Common.Dto;
using Common.Dto.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICountryService
    {
        Task<Message> CreateCountryAsMessageAsync(CountryCreateDto countryCreateDto, CancellationToken cancellationToken);
        Task<Message> GetCountryAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> GetCountriesAsMessageAsync( CancellationToken cancellationToken);

    }
}
