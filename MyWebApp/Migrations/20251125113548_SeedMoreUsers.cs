using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Z3w9zUrZJxzeYSGLTmvQJ.PlT4G249IKXatITBxR0xySgq00W4KC6");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 2, "$2a$11$fYpUMaMcDz82rF7MUwL7KuKuWP6ftx4W5o5YP6Mw9HbEHEhy01.FS", "joya" },
                    { 3, "$2a$11$4TJTtxCaIAw7pKlRUmolK..RwqkJeqbiSCD6CevhstfrgVDqOP.Pa", "mary" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$mCMhcFZ1CfD7PzcYP7tE..lMIjQAVPXnZz6o2Focmw.Ler.X5LTPm");
        }
    }
}
