using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4c97d65-06fd-47b6-b07f-43caca199191", "AQAAAAIAAYagAAAAENJaUgdFlo2pFDmwitwFzocxoAl7Vs5On9txMZt3FXFBPhG6mGtpOI7GIoHUe6pNJw==", "3a473f68-f675-44f1-bef6-2b2130ecbadf" });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Products_ProductId",
                table: "Inventory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Products_ProductId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e29a63c6-56a4-4a1d-8bb1-dacd7c9909c1", "AQAAAAIAAYagAAAAEGLtU1mk9l3FrwRyjGq4i18uZ8cg9tX4DQLLZ6Hirq+79PWSo/Y9vHW75KmQZo4wgQ==", "95bedcce-6172-41b9-916b-e73d7b3351d1" });
        }
    }
}
