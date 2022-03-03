using Microsoft.EntityFrameworkCore.Migrations;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    public partial class Configuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Key", "Description", "Value" },
                values: new object[,]
                {
                    { "FtpDownloadRoot", "ftp download url", "ftp download url" },
                    { "FtpUploadRoot", "ftp upload url", "ftp upload url" },
                    { "FtpPhotoSubfolder", "ftp subfolder for images", "img/" },
                    { "FtpUser", "ftp_login", "ftp login" },
                    { "FtpPassword", "password", "ftp password" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_LogoId",
                table: "Tournaments",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Files_LogoId",
                table: "Tournaments",
                column: "LogoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Files_LogoId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_LogoId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Tournaments");
        }
    }
}
