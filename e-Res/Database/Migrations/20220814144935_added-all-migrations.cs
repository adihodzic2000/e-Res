using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class addedallmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.CreateTable(
                name: "asp_net_roles",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "asp_net_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                schema: "identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "asp_net_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "asp_net_users",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    Clicked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_asp_net_users_UserFromId",
                        column: x => x.UserFromId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chat_asp_net_users_UserToId",
                        column: x => x.UserToId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfilePictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_asp_net_users_UserProfilePictureId",
                        column: x => x.UserProfilePictureId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Verifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verifications_asp_net_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Images_UserId",
                        column: x => x.UserId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApartment = table.Column<bool>(type: "bit", nullable: false),
                    IsHotel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Images_LogoId",
                        column: x => x.LogoId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Guests_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Guests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rooms_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rooms_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    TotalAmountOfNights = table.Column<double>(type: "float", nullable: false),
                    TotalAmountOfServices = table.Column<double>(type: "float", nullable: false),
                    PriceOfNight = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationServices_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservationServices_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservationServices_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_role_claims_RoleId",
                schema: "identity",
                table: "asp_net_role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "identity",
                table: "asp_net_roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_claims_UserId",
                schema: "identity",
                table: "asp_net_user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_logins_UserId",
                schema: "identity",
                table: "asp_net_user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_roles_RoleId",
                schema: "identity",
                table: "asp_net_user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "identity",
                table: "asp_net_users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_users_CompanyId",
                schema: "identity",
                table: "asp_net_users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "identity",
                table: "asp_net_users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CompanyId",
                table: "Bills",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CreatedByUserId",
                table: "Bills",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ModifiedByUserId",
                table: "Bills",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ReservationId",
                table: "Bills",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserFromId",
                table: "Chat",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserToId",
                table: "Chat",
                column: "UserToId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CreatedByUserId",
                table: "Cities",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ModifiedByUserId",
                table: "Cities",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationId",
                table: "Companies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoId",
                table: "Companies",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CreatedByUserId",
                table: "Countries",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ModifiedByUserId",
                table: "Countries",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserId",
                table: "Emails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_CompanyId",
                table: "Guests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_CreatedByUserId",
                table: "Guests",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_ModifiedByUserId",
                table: "Guests",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CreatedByUserId",
                table: "Images",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ModifiedByUserId",
                table: "Images",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserProfilePictureId",
                table: "Images",
                column: "UserProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CityId",
                table: "Locations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CreatedByUserId",
                table: "Locations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ModifiedByUserId",
                table: "Locations",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CreatedByUserId",
                table: "Reservations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestId",
                table: "Reservations",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ModifiedByUserId",
                table: "Reservations",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationServices_CreatedByUserId",
                table: "ReservationServices",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationServices_ModifiedByUserId",
                table: "ReservationServices",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationServices_ReservationId",
                table: "ReservationServices",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationServices_ServiceId",
                table: "ReservationServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyId",
                table: "Reviews",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatedByUserId",
                table: "Reviews",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ModifiedByUserId",
                table: "Reviews",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CompanyId",
                table: "Rooms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CreatedByUserId",
                table: "Rooms",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ModifiedByUserId",
                table: "Rooms",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatedByUserId",
                table: "Services",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ModifiedByUserId",
                table: "Services",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_UserId",
                table: "Verifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_claims_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_claims",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_logins_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_logins",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_roles_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_roles",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_tokens_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_tokens",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_users_Companies_CompanyId",
                schema: "identity",
                table: "asp_net_users",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_asp_net_users_CreatedByUserId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_asp_net_users_ModifiedByUserId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_asp_net_users_CreatedByUserId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_asp_net_users_ModifiedByUserId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_asp_net_users_CreatedByUserId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_asp_net_users_ModifiedByUserId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_asp_net_users_UserProfilePictureId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_asp_net_users_CreatedByUserId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_asp_net_users_ModifiedByUserId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "asp_net_role_claims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "asp_net_user_claims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "asp_net_user_logins",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "asp_net_user_roles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "asp_net_user_tokens",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "ReservationServices");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Verifications");

            migrationBuilder.DropTable(
                name: "asp_net_roles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "asp_net_users",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
