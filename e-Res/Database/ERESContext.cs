using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        }
    }
}