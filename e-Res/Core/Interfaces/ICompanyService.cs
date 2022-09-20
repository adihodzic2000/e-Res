using Common.Dto.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICompanyService
    {
        Task<Message> CreateCompanyAsMessage(CompanyCreateDto companyCreateDto, CancellationToken cancellationToken);
        Task<Message> GetCompanyAsMessage(Guid Id, CancellationToken cancellationToken);
        Task<Message> GetCompaniesRecommenderAsMessage(Guid CountryId, Guid CityId, bool IsApartment, bool IsHotel, CancellationToken cancellationToken);
        Task<Message> GetCompaniesAsMessage(Guid CountryId, Guid CityId, bool IsApartment, bool IsHotel, CancellationToken cancellationToken);
        Task<Message> UpdateCompanyAsMessage(Guid Id, CompanyUpdateDto companyUpdateDto, CancellationToken cancellationToken);
        //Task<Message> AddUserToCompanyAsMessage(AddUserToCompanyDto addUserToCompanyDto, CancellationToken cancellationToken);
    }
}
