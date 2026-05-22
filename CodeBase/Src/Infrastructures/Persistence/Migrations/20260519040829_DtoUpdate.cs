using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DtoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f8e22ac-1036-4c8e-a9ff-799fee03f0f4", "AQAAAAIAAYagAAAAEPjm3LDqcYw0r4a2phvoOBC7M4+UOUf73Lju1xqTP+Irp6wCfJ7benjagfkxRKTqQg==", "124fb69d-5a48-4a0d-9d11-c0f75bc209b4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8fa4a693-4fce-4850-9695-1d0c867a7e1b", "AQAAAAIAAYagAAAAEIw4dZ0anLa8AF9t3+BohEiE7LP0toPGMu9RZI77bfP1W25DuPsIpFkcCU9D42atEg==", "0e442fd5-9a4e-4173-9633-28ccc7602c08" });
        }
    }
}
