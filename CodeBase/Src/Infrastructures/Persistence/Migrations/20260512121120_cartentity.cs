using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cartentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "CartItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38c2f3ae-c462-4816-bc6f-3bc4ba405470", "AQAAAAIAAYagAAAAEJLECqlPxZKn9msmVP6Uo856ZVWtQiJ2kIuIwTCd0p/modKbhCRADXw/T6m+y5gL7A==", "2099e270-232c-49ef-a3ba-fd3079cc5f68" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippedAt",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2a582b2-6550-4888-8cdb-878fe1c0253b", "AQAAAAIAAYagAAAAEPdeTI8MqjTwn6rLkwqq7YP4h/4LcDSdMBrvLGxXXyL8a9bCZChWrM7ti9uAnanKKw==", "e984e999-cca0-48cd-a64d-ac42ec1b33bc" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
