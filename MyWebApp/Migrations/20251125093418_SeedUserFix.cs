using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { 1, "$2a$11$mCMhcFZ1CfD7PzcYP7tE..lMIjQAVPXnZz6o2Focmw.Ler.X5LTPm", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
