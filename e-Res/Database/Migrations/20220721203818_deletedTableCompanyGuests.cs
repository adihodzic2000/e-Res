using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class deletedTableCompanyGuests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyGuests");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Guests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Guests_CompanyId",
                table: "Guests",
                column: "CompanyId");

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropIndex(
                name: "IX_Guests_CompanyId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Guests");

            migrationBuilder.CreateTable(
                name: "CompanyGuests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGuests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyGuests_asp_net_users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyGuests_asp_net_users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalSchema: "identity",
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyGuests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyGuests_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGuests_CompanyId",
                table: "CompanyGuests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGuests_CreatedByUserId",
                table: "CompanyGuests",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGuests_GuestId",
                table: "CompanyGuests",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGuests_ModifiedByUserId",
                table: "CompanyGuests",
                column: "ModifiedByUserId");
        }
    }
}
