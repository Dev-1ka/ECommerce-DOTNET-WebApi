using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CartSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbc69a07-ce17-42bd-a6e1-f2815ddefd20", "AQAAAAIAAYagAAAAEEtOAX2E+awziOd32AtMg92/D+DIIOic/NbR71ucoik4PdUPceytf0VGu7VfMQbOdw==", "186e8321-86ba-4a7e-940a-59189d1e7979" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb3a3e15-efa3-482c-b510-6901bff7cede", "AQAAAAIAAYagAAAAEEi7+cLgwIGOIOg/+aUmNYumZw/NLmiOKfc0cgwMO0EWE8X/xpRDfKvE6FYUHi1zWA==", "05e16120-89e6-49cf-a68b-a56af492f873" });
        }
    }
}
