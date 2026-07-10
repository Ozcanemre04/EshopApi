using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductDataUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpireTime", "Refreshtoken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "02174cf9–9412–4cfe-afbf-59f706d72cf3", 0, "0caaf053-d2c0-442e-9206-a2b8541ddc18", "emre-1030@live.be", false, "Emre", "Ozcan", false, null, "EMRE-1030@LIVE.BE", "EMRE", "AQAAAAIAAYagAAAAEBlU2tIa8NDFCx2riOFyj4E3j8B+tBFsHUjhd2f2863P/F4PFdMAdRt9Dq1XfU7rQg==", null, false, new DateTime(2026, 7, 4, 19, 58, 20, 532, DateTimeKind.Utc).AddTicks(5571), "shi12yew8971237y2918yh912qy3741271syu129873", "566cab53-f18f-465f-a0cf-5f738dcae6bf", false, "Emre" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2169));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2174));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2175));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedDate",
                value: new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2175));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2235), "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2245), "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2246), "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2247), "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2249), "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2269), "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2271), "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2272), "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2273), "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2275), "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2276), "https://fakestoreapi.com/img/71kWymZ+c+L._AC_SX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2277), "https://fakestoreapi.com/img/61mtL65D4cL._AC_SX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2278), "https://fakestoreapi.com/img/81QpkIctqPL._AC_SX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2279), "https://fakestoreapi.com/img/81Zt42ioCgL._AC_SX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2280), "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2281), "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2283), "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2284), "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2286), "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2026, 6, 29, 19, 58, 20, 579, DateTimeKind.Utc).AddTicks(2287), "https://fakestoreapi.com/img/61pHAEJ4NML._AC_UX679_t.png" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "02174cf9–9412–4cfe-afbf-59f706d72cf3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "02174cf9–9412–4cfe-afbf-59f706d72cf3" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf9–9412–4cfe-afbf-59f706d72cf3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpireTime", "Refreshtoken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "69ca6479-cf17-47c3-9197-470df59fa7ab", "Admin@live.be", false, "admin", "admin", false, null, "ADMIN@LIVE.BE", "ADMIN", "AQAAAAIAAYagAAAAEB0RKu7jptz9N5GcO/vccK1pytaTDtJAWdKzWtPsStgMag1T7+l8s65CO6vCBMqTvQ==", null, false, new DateTime(2024, 12, 10, 16, 20, 29, 140, DateTimeKind.Utc).AddTicks(172), "Db2zYJwl9bOIJ+3Jn2HZEo6YLaEwMYjgt5t5RAwurY7SIxhTPDCXIymfGUqUvehcVXZMguLYHlHDqfMDfrqZtA==", "0f3ab092-880c-4696-b66b-24c89ad28170", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4096));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4125));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4127));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4189), "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4200), "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4203), "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4206), "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4209), "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4227), "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4229), "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4231), "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4233), "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4237), "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4239), "https://fakestoreapi.com/img/71kWymZ+c+L._AC_SX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4241), "https://fakestoreapi.com/img/61mtL65D4cL._AC_SX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4244), "https://fakestoreapi.com/img/81QpkIctqPL._AC_SX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4246), "https://fakestoreapi.com/img/81Zt42ioCgL._AC_SX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4248), "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4250), "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4252), "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4256), "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4258), "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedDate", "Image" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4261), "https://fakestoreapi.com/img/61pHAEJ4NML._AC_UX679_.jpg" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "8e445865-a24d-4543-a6c6-9443d048cdb9" });
        }
    }
}
