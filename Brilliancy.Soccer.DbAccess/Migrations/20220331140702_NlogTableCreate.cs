using Brilliancy.Soccer.Common.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    public partial class NlogTableCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(ResourceHelper.GetResourceAsString("NLogTableInit.sql", typeof(NlogTableCreate)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(ResourceHelper.GetResourceAsString("NLogTableInitDrop.sql", typeof(NlogTableCreate)));
        }
    }
}
