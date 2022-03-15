using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidCertificate.Data.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "5d933833-cd01-41cf-adf4-2c33371711fc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "SchoolAdminRoleId",
                column: "ConcurrencyStamp",
                value: "cd50a42d-c574-4afc-8267-7f11fc969e2a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "StudentRoleId",
                column: "ConcurrencyStamp",
                value: "71e806c1-d311-4e1c-84bb-7531615e3a89");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "TeacherRoleId",
                column: "ConcurrencyStamp",
                value: "17d0845a-8a07-4422-a075-bf76f2ace5e7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0eeb3aa4-cdc7-4fcf-92ed-38feec3c0a12", "AQAAAAEAACcQAAAAEKtLLe3gY9Itdaj2Fsu/cgzNL/I+dpSEdevzQYO8WmJo3t/XHkpiHk+nNoaeGBWpLQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AdminRoleId",
                column: "ConcurrencyStamp",
                value: "62ef7897-85e0-4d74-905f-7ec3a364899c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "SchoolAdminRoleId",
                column: "ConcurrencyStamp",
                value: "b42086f6-9010-48e9-8475-308c55cc4a5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "StudentRoleId",
                column: "ConcurrencyStamp",
                value: "20fdfbca-7753-48bc-914f-23732fdb0b38");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "TeacherRoleId",
                column: "ConcurrencyStamp",
                value: "e06ba9eb-031b-460e-9d85-669357de579f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "34d521da-16b0-49eb-9b5d-aa0fce90c93e", "AQAAAAEAACcQAAAAEFs+zf2cpQejXsyvtx76T7uPybiJrev3KSE5hKsQ0ojV1It86r6fOCURBfKd2IoKTw==" });
        }
    }
}
