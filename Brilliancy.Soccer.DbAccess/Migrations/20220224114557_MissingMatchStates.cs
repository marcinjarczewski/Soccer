using Microsoft.EntityFrameworkCore.Migrations;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    public partial class MissingMatchStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MatchStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Finished" });

            migrationBuilder.InsertData(
                table: "MatchStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Finished" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MatchStates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MatchStates",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
