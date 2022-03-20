using Microsoft.EntityFrameworkCore.Migrations;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    public partial class MissingAuthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuthenticationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "ResetPassword" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthenticationTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
