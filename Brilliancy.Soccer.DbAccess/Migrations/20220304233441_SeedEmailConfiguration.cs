using Microsoft.EntityFrameworkCore.Migrations;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    public partial class SeedEmailConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Key", "Description", "Value" },
                values: new object[,]
                {
                    { "EmailRegisterAddress", "Sender (no reply) email address", "soccer-noreply@brilliancy.pl" },
                    { "EmailRegisterName", "Email name shown to the user", "Soccer team" },
                    { "EmailRegisterPassword", "Password to the sender email", "xyz" },
                    { "EmailRegisterPort", "SMTP email port", "587" },
                    { "EmailRegisterReplyTo", "Email shown as reply to", "soccer-noreply@brilliancy.pl" },
                    { "EmailRegisterSMTP", "Smtp address", "smtp.webio.pl" },
                    { "EmailRegisterSSLEnabled", "Is SMTP use SSL", "true" },
                    { "EmailServiceSendingTime", "Time in minutes separated by ; between unsuccessful emails sent", "1;2;5;15;40;120" },
                    { "EmailServiceSleepTime", "Service sleep time", "60000" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailRegisterAddress");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailRegisterName");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailRegisterPassword");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailRegisterPort");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailRegisterReplyTo");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailRegisterSMTP");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailRegisterSSLEnabled");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailServiceSendingTime");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Key",
                keyValue: "EmailServiceSleepTime");
        }
    }
}
