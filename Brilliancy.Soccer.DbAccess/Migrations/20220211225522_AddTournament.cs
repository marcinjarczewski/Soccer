using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    public partial class AddTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultDayOfTheWeek = table.Column<int>(type: "int", nullable: true),
                    DefaultHour = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.CheckConstraint("CK_DefaultDayOfTheWeek", "[DefaultDayOfTheWeek] >= 1 AND [DefaultDayOfTheWeek] <= 7");
                    table.ForeignKey(
                        name: "FK_Tournaments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentAdmins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentAdmins", x => new { x.UserId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_TournamentAdmins_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentAdmins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentAdmins_TournamentId",
                table: "TournamentAdmins",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OwnerId",
                table: "Tournaments",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentAdmins");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
