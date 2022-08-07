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
using Common.Dto.Locations;

namespace Core.Services
{
    public class CompanyService : ICompanyService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        public IFileService fileService { get; set; }
        public ILocationService locationService { get; set; }
        public IRoomService roomService { get; set; }

        public CompanyService(ERESContext dbContext, IMapper mapper, IFileService fileService, ILocationService locationService, IRoomService roomService)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.fileService = fileService;
            this.locationService = locationService;
            this.roomService = roomService;
        }

        public async Task<Message> CreateCompanyAsMessage(CompanyCreateDto companyCreateDto, CancellationToken cancellationToken)
        {
            try
            {

                //FileUploadDto file = new FileUploadDto
                //{
                //    ImageURL = companyCreateDto.ImageURL
                //};
                //var message1 = await fileService.UploadImageAsMessageAsync(file, cancellationToken);
                //if (message1 == null || !message1.IsValid)
                //{
                //    return new Message
                //    {
                //        IsValid = false,
                //        Info = "Bad image",
                //        Status = ExceptionCode.BadRequest
                //    };
                //}

                LocationCreateDto locationCreateDto = new LocationCreateDto { CityId = companyCreateDto.CityId, Latitude = companyCreateDto.Latitude, Longitude = companyCreateDto.Longitude };
                var message2 = await locationService.CreateLocationAsMessageAsync(locationCreateDto, cancellationToken);
                if (message2 == null || !message2.IsValid)
                {
                    return new Message
                    {
                        IsValid = false,
                        Info = "Error during uploading a location!",
                        Status = ExceptionCode.BadRequest
                    };
                }

                var obj = Mapper.Map<Company>(companyCreateDto);
                obj.Id = Guid.NewGuid();
                obj.CreatedDate = DateTime.Now;
                //obj.LogoId = ((ImageGetDto)message1.Data).Id;
                obj.LocationId = ((LocationGetDto)message2.Data).Id;

                await _dbContext.Companies.AddAsync(obj);
                await _dbContext.SaveChangesAsync(cancellationToken);
                if (companyCreateDto.IsApartment)
                {
                    Common.Dto.Rooms.RoomCreateDto roomCreateDto = new Common.Dto.Rooms.RoomCreateDto();
                    roomCreateDto.Description = "N/A";
                    roomCreateDto.Price = 30;
                    roomCreateDto.Title = companyCreateDto.Title;
                    roomCreateDto.CompanyId = obj.Id;
                    roomCreateDto.Color = "#0066c0";
                    await _dbContext.Rooms.AddAsync(Mapper.Map<Room>(roomCreateDto));
                    await _dbContext.SaveChangesAsync();
                }


                return new Message
                {
                    IsValid = true,
                    Info = "Successfully added company",
                    Status = ExceptionCode.Success,
                    Data = obj
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

        public async Task<Message> GetCompanyAsMessage(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _dbContext.Companies.Include(x => x.Logo).Include(x => x.Location).Include(x => x.Location.City).Include(x => x.Location.City.Country).AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);


                var obj = Mapper.Map<CompanyGetDto>(company);
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully got company",
                    Status = ExceptionCode.Success,
                    Data = obj
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

        public async Task<Message> UpdateCompanyAsMessage(Guid Id, CompanyUpdateDto companyUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _dbContext.Companies.Include(x => x.Location).Include(x => x.Location.City).Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);

                Mapper.Map(companyUpdateDto, company);

                var location = await _dbContext.Locations.Where(x => x.Latitude == companyUpdateDto.Latitude && x.Longitude == companyUpdateDto.Longitude).FirstOrDefaultAsync(cancellationToken);
                if (location == null)
                {
                    LocationCreateDto locationCreateDto = new LocationCreateDto { CityId = companyUpdateDto.CityId, Latitude = companyUpdateDto.Latitude, Longitude = companyUpdateDto.Longitude };
                    var message2 = await locationService.CreateLocationAsMessageAsync(locationCreateDto, cancellationToken);
                    if (message2 == null || !message2.IsValid)
                    {
                        return new Message
                        {
                            IsValid = false,
                            Info = "Error during uploading a location!",
                            Status = ExceptionCode.BadRequest
                        };
                    }
                    company.LocationId = ((LocationGetDto)message2.Data).Id;
                }
                company.Location.CityId = companyUpdateDto.CityId;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Message
                {
                    IsValid = true,
                    Info = "Successfully updated company",
                    Status = ExceptionCode.Success,
                    Data = company
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

        public async Task<Message> GetCompaniesAsMessage(Guid CountryId, Guid CityId, bool IsApartment, bool IsHotel, CancellationToken cancellationToken)
        {
            try
            {
                var companies = await _dbContext.Companies
                    .Include(x => x.Location)
                    .Include(x => x.Location.City)
                    .Include(x => x.Location.City.Country)
                    .Include(x => x.Logo)
                    .Where(x =>
                    (CountryId == Guid.Empty || CountryId == x.Location.City.CountryId) &&
                    (CityId == Guid.Empty || CityId == x.Location.CityId) &&
                    ((IsApartment && IsHotel && (x.IsHotel || x.IsApartment))
                    || ((!IsApartment && !x.IsApartment) || (IsApartment && x.IsApartment))
                    || ((!IsHotel && !x.IsHotel) || (IsHotel && x.IsHotel))
                    ) 
                   ).ToListAsync(cancellationToken);

                var convertedCompanies = Mapper.Map<List<CompanyGetDtoWithReview>>(companies);

                foreach (var company in convertedCompanies)
                {
                    if (await _dbContext.Reviews.Where(x => x.CompanyId == company.Id).CountAsync(cancellationToken) > 0)
                        company.AvgReview = await _dbContext.Reviews.Where(x => x.CompanyId == company.Id).AverageAsync(x => x.Grade);
                }
                convertedCompanies = convertedCompanies.OrderByDescending(x => x.AvgReview).ToList();
                return new Message
                {
                    IsValid = true,
                    Info = "Successfully returned data",
                    Status = ExceptionCode.Success,
                    Data = convertedCompanies
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

        //public async Task<Message> AddUserToCompanyAsMessage(AddUserToCompanyDto addUserToCompanyDto, CancellationToken cancellationToken)
        //{
        //    try
        //    {

        //        var company = await _dbContext.Companies.Where(x => x.Id == addUserToCompanyDto.CompanyId).FirstOrDefaultAsync(cancellationToken);
        //        var user= await _dbContext.Users.Where(x => x.Id == addUserToCompanyDto.UserId).FirstOrDefaultAsync(cancellationToken);

        //        bool existsInCompany = await _dbContext.CompanyUsers.Where(x => x.UserId == addUserToCompanyDto.UserId && x.CompanyId == addUserToCompanyDto.CompanyId).AnyAsync(cancellationToken);

        //        if(user==null || company == null)
        //        {
        //            return new Message
        //            {
        //                IsValid = false,
        //                Info = "You didn't send company id or user id !",
        //                Status = ExceptionCode.BadRequest
        //            };
        //        }
        //        if (existsInCompany)
        //        {
        //            return new Message
        //            {
        //                IsValid = false,
        //                Info = $"User exists in company {company.Title} already!",
        //                Status = ExceptionCode.BadRequest
        //            };
        //        }
        //        var obj = Mapper.Map<CompanyUsers>(addUserToCompanyDto);

        //        await _dbContext.CompanyUsers.AddAsync(obj);
        //        await _dbContext.SaveChangesAsync(cancellationToken);

        //        return new Message
        //        {
        //            IsValid = true,
        //            Info = $"Successfully added user to company: {company.Title}",
        //            Status = ExceptionCode.Success
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Message
        //        {
        //            IsValid = false,
        //            Info = ex.Message,
        //            Status = ExceptionCode.BadRequest
        //        };
        //    }
        //}
    }
}
