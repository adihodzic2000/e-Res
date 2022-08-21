using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ERESContext : IdentityDbContext<User, Role, Guid>, IERESContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        //public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationServices> ReservationServices { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Verifications> Verifications { get; set; }
        //public DbSet<CompanyUsers> CompanyUsers { get; set; }

        public ERESContext(DbContextOptions<ERESContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("asp_net_users", "identity");
            modelBuilder.Entity<Role>().ToTable("asp_net_roles", "identity");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("asp_net_user_claims", "identity");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("asp_net_user_roles", "identity");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("asp_net_user_logins", "identity");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("asp_net_role_claims", "identity");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("asp_net_user_tokens", "identity");

            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    Id = new Guid("096AF8D7-251B-4BE5-581C-08DA6A7BE48A"),
            //    FirstName = "string123",
            //    LastName = "string123",
            //    Gender = Entities.Enums.Gender.Female,
            //    IsDeleted =false,
            //    CreatedDate = DateTime.Now,
            //    //RefreshToken = NULL,
            //    //RefreshTokenExpireDate = "Test",
            //    //CompanyId = "test@fit.ba",
            //    UserName = "string",
            //    NormalizedUserName = "STRING",
            //    Email = "string123",
            //    NormalizedEmail = "STRING123",
            //    EmailConfirmed = false,
            //    PasswordHash = "AQAAAAEAACcQAAAAEM3Q3Hec3PtmWHXzzQCVZ4c1aqPUyyUIcdxccgaQ+rOwdJYK6p6GF+E5azb13mb3aA==",
            //    SecurityStamp = "EZAPJEQCWPYZIE5VD5BPWMQH5QAGI4I4",
            //    ConcurrencyStamp = "0b934053-54b8-4dce-9934-9c6efea29e33",
            //    PhoneNumber = "string123",
            //    PhoneNumberConfirmed = false,
            //    TwoFactorEnabled = false,
            //    //LockoutEnd = "test@fit.ba",
            //    LockoutEnabled = true,
            //    AccessFailedCount = 0
            //});
            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    Id = new Guid("CEECE2E6-4966-4652-A711-08DA6B3BD31E"),
            //    FirstName = "Adi",
            //    LastName = "Hodzic",
            //    Gender = Entities.Enums.Gender.Male,
            //    IsDeleted = false,
            //    CreatedDate = DateTime.Now,
            //    //RefreshToken = NULL,
            //    //RefreshTokenExpireDate = "Test",
            //    //CompanyId = "test@fit.ba",
            //    UserName = "adi",
            //    NormalizedUserName = "ADI",
            //    Email = "adihodzic94@gmail.com",
            //    NormalizedEmail = "ADIHODZIC94@GMAIL.COM",
            //    EmailConfirmed = false,
            //    PasswordHash = "AQAAAAEAACcQAAAAEDa1Zm+99qKqq+HljfWu+iaEKk5XSExIAv/BPX601aatad9i4SKT3qIgDBqHDikfSA==",
            //    SecurityStamp = "GA6TR7STEZP7NNAPO7SY77P5ZDL7CYII",
            //    ConcurrencyStamp = "e79f99a6-c73a-4079-a4ed-751e82405e15",
            //    PhoneNumber = "0626100682",
            //    PhoneNumberConfirmed = false,
            //    TwoFactorEnabled = false,
            //    //LockoutEnd = "test@fit.ba",
            //    LockoutEnabled = true,
            //    AccessFailedCount = 0
            //});
            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    Id = new Guid("210516D0-474A-4DB6-A5A5-08DA6E72CEC8"),
            //    FirstName = "mobile",
            //    LastName = "mobile",
            //    Gender = Entities.Enums.Gender.Male,
            //    IsDeleted = false,
            //    CreatedDate = DateTime.Now,
            //    //RefreshToken = NULL,
            //    //RefreshTokenExpireDate = "Test",
            //    //CompanyId = "test@fit.ba",
            //    UserName = "mobile",
            //    NormalizedUserName = "MOBILE",
            //    Email = "mobile",
            //    NormalizedEmail = "MOBILE",
            //    EmailConfirmed = false,
            //    PasswordHash = "AQAAAAEAACcQAAAAEKASxsYslNq2DsKqZFuG8UEos0ukNfO+lZGpVoETlPOzsq7s8hicTlLQcrDtk9SZTw==+rOwdJYK6p6GF+E5azb13mb3aA==",
            //    SecurityStamp = "3UAE2RHL5SOVHVYUOAZPS5US5CPSM5FO",
            //    ConcurrencyStamp = "8d2f3c48-e585-4cde-9ac1-991de590d9f6",
            //    PhoneNumber = "7p3l25Cnbg+2QxoQRElFJjIqHgA=",
            //    PhoneNumberConfirmed = false,
            //    TwoFactorEnabled = false,
            //    //LockoutEnd = "test@fit.ba",
            //    LockoutEnabled = true,
            //    AccessFailedCount = 0
            //});
            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    Id = new Guid("F24E8DF0-B5C7-461E-13D7-08DA73D9DF28"),
            //    FirstName = "adi_ap",
            //    LastName = "adi_ap",
            //    Gender = Entities.Enums.Gender.Female,
            //    IsDeleted = false,
            //    CreatedDate = DateTime.Now,
            //    //RefreshToken = NULL,
            //    //RefreshTokenExpireDate = "Test",
            //    //CompanyId = "test@fit.ba",
            //    UserName = "adi_ap",
            //    NormalizedUserName = "ADI_AP",
            //    Email = "sgakls94@gmail.com",
            //    NormalizedEmail = "SGAKLS94@GMAIL.COM",
            //    EmailConfirmed = false,
            //    PasswordHash = "AQAAAAEAACcQAAAAENGSSuML//qTkDVBw7Y04OszWzkQFew8RaMhcZy6uQIPUeeLAsHUxq7S8yADdiTelQ==+rOwdJYK6p6GF+E5azb13mb3aA==",
            //    SecurityStamp = "P4WFS3OBA34F25FOGRHBP6UUCGKTOUX5",
            //    ConcurrencyStamp = "8cc0b406-ea1c-4d30-9552-bc8dddf6d181",
            //    PhoneNumber = "7p3l25Cnbg+2QxoQRElFJjIqHgA=",
            //    PhoneNumberConfirmed = false,
            //    TwoFactorEnabled = false,
            //    //LockoutEnd = "test@fit.ba",
            //    LockoutEnabled = true,
            //    AccessFailedCount = 0
            //});

        }
    }
}


