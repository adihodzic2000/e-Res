using Common.Dto.Guests;
using Common.Dto.Role;
using Common.Dtos.Services;
using Core.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IServicesService
    {
        Task<Message> CreateServiceAsMessageAsync(ServicesCreateDto serviceCreateDto, CancellationToken cancellationToken);
        Task<Message> UpdateServiceAsMessageAsync(Guid serviceId, ServicesUpdateDto serviceUpdateDto, CancellationToken cancellationToken);
        Task<Message> DeleteServiceAsMessageAsync(Guid serviceId, CancellationToken cancellationToken);
        Task<Message> GetServiceAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> GetServicesByCompanyIdAsMessageAsync(BaseSearchObject baseSearch, CancellationToken cancellationToken);
        Task<Message> AddServiceToGuestIdAsMessageAsync(AddServiceToGuest addServiceToGuest, CancellationToken cancellationToken);
    }
}
