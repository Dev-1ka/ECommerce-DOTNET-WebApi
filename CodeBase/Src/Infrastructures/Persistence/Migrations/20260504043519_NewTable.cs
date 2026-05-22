using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54eafd4b-fd47-4102-a84c-46710e57bd0e", "AQAAAAIAAYagAAAAEDC6gCOEm3x8CTrkfjTfq2SawRk+THkaVlrQhotIPZTicQWHjzg5qdefdQ1Wn+sOGQ==", "123dd06d-6903-4a57-85aa-18a29c9766ad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "427255fc-75c3-4cf3-9cdd-ab1da98c95fa", "AQAAAAIAAYagAAAAEKgmIDr0AGJtYeJr8qR+5qEcQsau3R1hTGbP/B1bWFT/7Im33Vl6lqM2Jdd6D03cIg==", "1784a220-a802-4184-9e26-4c7c31b50d43" });
        }
    }
}
