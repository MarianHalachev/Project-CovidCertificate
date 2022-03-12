using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidCertificate.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "ba46a904-7ee9-4830-9fea-4c2fe5bd12aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "UserRoleId",
                column: "ConcurrencyStamp",
                value: "2ef31a9a-4f66-48d3-88e5-45a78ab89dcb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2ee9f351-aeef-4658-a43c-78732871970b", "AQAAAAEAACcQAAAAEOZv8Q7DUm3t851SnCQorYUR9UZCAfr5vOBcUrYV5fkATpdUexAcdpOwkFxQmVz8pA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "daca2889-c1f8-4936-abfb-c9fce99c17b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "UserRoleId",
                column: "ConcurrencyStamp",
                value: "2bb66e44-2cd1-4c47-9757-8a9efd62e6d0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f04f5e1a-7329-4c80-97f7-02c7052e5f42", "AQAAAAEAACcQAAAAEAMNQqKP4Nb9p4dgAi7SLVJZJ+LuC3uKVTcku7FPj/yHFYG8G8af79rgZXCz5DZmfw==" });
        }
    }
}
