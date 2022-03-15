using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidCertificate.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certificate_UserId",
                table: "Certificate");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "ace920f9-1958-4cf6-a533-da2412637bee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "UserRoleId",
                column: "ConcurrencyStamp",
                value: "75c510c5-aecc-463c-99e4-03a1e59b238d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c156191f-3154-46cb-923a-e8f9bc3335d3", "AQAAAAEAACcQAAAAEFT41zcqnqwIQZ42W7qTY1UzxW7zDlZy7M0LtvbiYOdPoNvZZenPe5luMX80z9yZGQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_UserId",
                table: "Certificate",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certificate_UserId",
                table: "Certificate");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "bedf7acf-a4ed-4d29-9140-b90cac6d537e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "UserRoleId",
                column: "ConcurrencyStamp",
                value: "610a3501-f8c1-455d-b8b6-9944cf51cf8b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5fee4532-4e9d-440f-9c78-af69e49183e1", "AQAAAAEAACcQAAAAEPc+QreQa2L9Uy0y+1zQTwN6hsiaeT640atiKIoHwSvss/Rq7ZROzrxAVnKLJ+3gGw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_UserId",
                table: "Certificate",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}
