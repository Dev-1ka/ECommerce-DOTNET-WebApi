using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb3a3e15-efa3-482c-b510-6901bff7cede", "AQAAAAIAAYagAAAAEEi7+cLgwIGOIOg/+aUmNYumZw/NLmiOKfc0cgwMO0EWE8X/xpRDfKvE6FYUHi1zWA==", "05e16120-89e6-49cf-a68b-a56af492f873" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1fbf3be-e97b-45f8-8468-a358cf564d6f", "AQAAAAIAAYagAAAAELJwpPM5mVtO4x0Z52ukO5OD6Y14mBu5u6SnHBxiydCs7Vd4GRhOtqNGb/8UqU9U3A==", "b8b6089d-85e9-4a1c-9bef-e9abe8c50040" });
        }
    }
}
