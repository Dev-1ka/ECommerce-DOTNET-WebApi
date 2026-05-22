using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Cartproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdeed236-62cc-49c2-a277-6ff87badf488", "AQAAAAIAAYagAAAAEBxY4pWwt49zqXDHFLhi/fJsBjEpC/jIY0Zj87fpxPMfvL8xz0L+7yBzxz7Z2iAdHQ==", "6d4fc69e-f76c-4313-9b2b-694e70dc1f4e" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbc69a07-ce17-42bd-a6e1-f2815ddefd20", "AQAAAAIAAYagAAAAEEtOAX2E+awziOd32AtMg92/D+DIIOic/NbR71ucoik4PdUPceytf0VGu7VfMQbOdw==", "186e8321-86ba-4a7e-940a-59189d1e7979" });
        }
    }
}
