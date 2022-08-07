using AutoMapper;
using Common.Dto.Bills;
using Common.Dto.City;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Guests;
using Common.Dto.Images;
using Common.Dto.Locations;
using Common.Dto.Reservations;
using Common.Dto.ReservationServices;
using Common.Dto.Role;
using Common.Dto.Rooms;
using Common.Dto.User;
using Common.Dtos.Chat;
using Common.Dtos.Reservations;
using Common.Dtos.Reviews;
using Common.Dtos.Services;
using Common.Dtos.Verification;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //================COMPANIES===============
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();
            CreateMap<Company, CompanyGetDto>();
            CreateMap<Company, CompanyGetDtoWithReview>();
            //========================================

            //================IMAGES==================
            CreateMap<ImageCreateDto, Images>();
            CreateMap<Images, ImageGetDto>();
            //========================================

            //================COUNTRIES===============
            CreateMap<CountryCreateDto, Country>();
            CreateMap<Country, CountryGetDto>();
            //========================================

            //================CITIES==================
            CreateMap<CityCreateDto, City>();
            CreateMap<City, CityGetDto>();
            //========================================

            //================LOCATIONS===============
            CreateMap<LocationCreateDto, Location>();
            CreateMap<LocationUpdateDto, Location>();
            CreateMap<Location, LocationGetDto>();
            //========================================

            //================ROLES===================
            CreateMap<RoleCreateDto, Role>();
            //========================================

            //================GUESTS==================
            CreateMap<GuestCreateDto, Guest>();
            CreateMap<GuestUpdateDto, Guest>();
            CreateMap<Guest, GuestGetDto>();
            //========================================

            //================SERVICES================
            CreateMap<ServicesCreateDto, Service>();
            CreateMap<ServicesUpdateDto, Service>();
            CreateMap<Service, ServicesGetDto>();
            //========================================

            //================ROOMS===================
            CreateMap<RoomCreateDto, Room>();
            CreateMap<RoomUpdateDto, Room>();
            CreateMap<Room, RoomGetDto>();
            //========================================

            //================CHAT====================
            CreateMap<CreateMessageDto, Chat>();
            CreateMap<Chat, GetMessageDto>();
            //========================================

            //================USERS===================
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserGetDto>();
            //========================================

            //================RESERVATIONS============
            CreateMap<FinishReservationDto, Reservation>();
            CreateMap<ReservationCreateDto, Reservation>();
            CreateMap<ReservationUpdateDto, Reservation>();
            CreateMap<Reservation, ReservationGetDto>();
            //========================================

            //=========RESERVATION SERVICES===========
            CreateMap<AddServiceToGuest, Entities.ReservationServices>();
            CreateMap<Entities.ReservationServices, ReservationServicesGetDto>();
            CreateMap<ReservationServicesGetDto, Entities.ReservationServices>();
            //========================================

            //================BILLS===================
            CreateMap<BillsCreateDto, Bill>();
            CreateMap<BillsPayDto, Bill>();
            CreateMap<BillsUpdateDto, Bill>();
            CreateMap<Bill, BillsGetDto>();
            //========================================

            //================VERIFICATIONS===========
            CreateMap<Verifications, VerificationGetDto>();
            //========================================

            //================REVIEWS=================
            CreateMap<ReviewCreateDto, Reviews>();
            CreateMap<Reviews, ReviewGetDto>();
            //========================================
        }
    }
}
