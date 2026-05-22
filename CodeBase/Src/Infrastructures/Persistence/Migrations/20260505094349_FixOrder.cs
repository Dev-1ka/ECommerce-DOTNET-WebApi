using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaidAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f62a6c3-ee9f-4f5b-a9c1-af2febfeb723", "AQAAAAIAAYagAAAAEFbdgFQK+r4BGdc1ikiwgNSQjoFHYr9NQ1dfqta2jI6Ypl3yiIB9Q4UXiifiHq8W4Q==", "7d167922-03da-487e-8a52-54171398b4d3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAt",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4c97d65-06fd-47b6-b07f-43caca199191", "AQAAAAIAAYagAAAAENJaUgdFlo2pFDmwitwFzocxoAl7Vs5On9txMZt3FXFBPhG6mGtpOI7GIoHUe6pNJw==", "3a473f68-f675-44f1-bef6-2b2130ecbadf" });
        }
    }
}
