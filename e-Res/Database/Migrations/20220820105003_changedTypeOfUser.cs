using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class changedTypeOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Images_UserId",
                table: "Emails");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_asp_net_users_UserId",
                table: "Emails",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_asp_net_users_UserId",
                table: "Emails");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Images_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
