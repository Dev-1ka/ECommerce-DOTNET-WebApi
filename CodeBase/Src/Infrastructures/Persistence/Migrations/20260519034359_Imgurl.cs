using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Imgurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8fa4a693-4fce-4850-9695-1d0c867a7e1b", "AQAAAAIAAYagAAAAEIw4dZ0anLa8AF9t3+BohEiE7LP0toPGMu9RZI77bfP1W25DuPsIpFkcCU9D42atEg==", "0e442fd5-9a4e-4173-9633-28ccc7602c08" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38c2f3ae-c462-4816-bc6f-3bc4ba405470", "AQAAAAIAAYagAAAAEJLECqlPxZKn9msmVP6Uo856ZVWtQiJ2kIuIwTCd0p/modKbhCRADXw/T6m+y5gL7A==", "2099e270-232c-49ef-a3ba-fd3079cc5f68" });
        }
    }
}
