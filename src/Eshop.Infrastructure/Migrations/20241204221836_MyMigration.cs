using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpireTime", "SecurityStamp" },
                values: new object[] { "acfbbcf7-c495-4161-9907-05f54f771afd", "AQAAAAIAAYagAAAAEJEOABL2/L7s6m7lazg4PvVUqUeJmDN3Bu/gd2pxpOhhFVsnz5owMdix5rckREUPJg==", new DateTime(2024, 12, 9, 22, 18, 36, 20, DateTimeKind.Utc).AddTicks(3941), "92c50598-f356-45e0-b2bf-f401c52b5e82" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8737));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8741));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CategoryName", "CreatedDate" },
                values: new object[] { "Jewelery", new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8743) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CategoryName", "CreatedDate" },
                values: new object[] { "Electronics", new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8743) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Image", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[] { 1L, 1L, new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8788), "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday", "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg", "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops", 109.95m, 120, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpireTime", "SecurityStamp" },
                values: new object[] { "6d90135a-32de-4238-8d3a-2ef544c37623", "AQAAAAIAAYagAAAAEKUeegsBaAqmPJJ8MKDOf4luoiyHyPlpclqY7WNV2JU9awa4KeD98JbV46ha/3aisQ==", new DateTime(2024, 11, 4, 19, 34, 42, 381, DateTimeKind.Utc).AddTicks(79), "55d002cd-236d-46cf-9e81-7ddbf5a62e88" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 19, 34, 42, 519, DateTimeKind.Utc).AddTicks(3786));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 19, 34, 42, 519, DateTimeKind.Utc).AddTicks(3791));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CategoryName", "CreatedDate" },
                values: new object[] { "Books", new DateTime(2024, 10, 30, 19, 34, 42, 519, DateTimeKind.Utc).AddTicks(3794) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CategoryName", "CreatedDate" },
                values: new object[] { "Smartphones", new DateTime(2024, 10, 30, 19, 34, 42, 519, DateTimeKind.Utc).AddTicks(3796) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 5L, "Computers", new DateTime(2024, 10, 30, 19, 34, 42, 519, DateTimeKind.Utc).AddTicks(3798), null },
                    { 6L, "Video Games", new DateTime(2024, 10, 30, 19, 34, 42, 519, DateTimeKind.Utc).AddTicks(3800), null }
                });
        }
    }
}
