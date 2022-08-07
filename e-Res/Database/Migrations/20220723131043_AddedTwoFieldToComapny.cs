using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class AddedTwoFieldToComapny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_claims_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_logins_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_tokens_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Companies_CompanyId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_asp_net_users_UserFromId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_asp_net_users_UserToId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Reservations_ReservationId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Images_UserId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CityId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Services_ServiceId",
                table: "ReservationServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Companies_CompanyId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Images_UserId",
                table: "Verifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsApartment",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHotel",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_role_claims",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "asp_net_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_user_roles",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "asp_net_roles",
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
                name: "FK_Bills_Companies_CompanyId",
                table: "Bills",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_asp_net_users_UserFromId",
                table: "Chat",
                column: "UserFromId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_asp_net_users_UserToId",
                table: "Chat",
                column: "UserToId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Reservations_ReservationId",
                table: "CreditCards",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Images_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Cities_CityId",
                table: "Locations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Services_ServiceId",
                table: "ReservationServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Companies_CompanyId",
                table: "Rooms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Images_UserId",
                table: "Verifications",
                column: "UserId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_claims_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_logins_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_tokens_asp_net_users_UserId",
                schema: "identity",
                table: "asp_net_user_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Companies_CompanyId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_asp_net_users_UserFromId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_asp_net_users_UserToId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Reservations_ReservationId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Images_UserId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CityId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationServices_Services_ServiceId",
                table: "ReservationServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Companies_CompanyId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Images_UserId",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "IsApartment",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsHotel",
                table: "Companies");

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_role_claims",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "asp_net_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                schema: "identity",
                table: "asp_net_user_roles",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "asp_net_roles",
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
                name: "FK_Bills_Companies_CompanyId",
                table: "Bills",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_asp_net_users_UserFromId",
                table: "Chat",
                column: "UserFromId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_asp_net_users_UserToId",
                table: "Chat",
                column: "UserToId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Reservations_ReservationId",
                table: "CreditCards",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Images_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Cities_CityId",
                table: "Locations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Guests_GuestId",
                table: "Reservations",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Reservations_ReservationId",
                table: "ReservationServices",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationServices_Services_ServiceId",
                table: "ReservationServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Companies_CompanyId",
                table: "Rooms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Images_UserId",
                table: "Verifications",
                column: "UserId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
