using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab5fd66f-39b1-46d6-9eaf-0ee06c83a8ae", "AQAAAAIAAYagAAAAEJgaCfZkH8nxPPEFqqtdFRjP+KoZSxFYVz21ridaM62xhjQNhq4BBpUxbxXqRgB1eg==", "3ec80a92-eeef-4c92-92ff-ed89cedb0ff9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "338c6a8d-05a3-4cbe-a918-47164b56d15b", "AQAAAAIAAYagAAAAEI2pQTDErvykqXZ/3SGlIDdnRSMloeOBNRM6QUKCo3PcJUkL+k0xzCPYLK4gY08coA==", "a4d5d330-3a31-4304-8be5-305e11657a1f" });
        }
    }
}
