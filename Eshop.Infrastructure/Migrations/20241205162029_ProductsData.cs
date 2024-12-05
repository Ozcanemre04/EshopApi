using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpireTime", "SecurityStamp" },
                values: new object[] { "69ca6479-cf17-47c3-9197-470df59fa7ab", "AQAAAAIAAYagAAAAEB0RKu7jptz9N5GcO/vccK1pytaTDtJAWdKzWtPsStgMag1T7+l8s65CO6vCBMqTvQ==", new DateTime(2024, 12, 10, 16, 20, 29, 140, DateTimeKind.Utc).AddTicks(172), "0f3ab092-880c-4696-b66b-24c89ad28170" });

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
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4189));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Image", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[,]
                {
                    { 2L, 1L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4200), "Slim-fitting style, contrast raglan long sleeve, three-button henley placket, light weight & soft fabric for breathable and comfortable wearing. And Solid stitched shirts with round neck made for durability and a great fit for casual fashion wear and diehard baseball fans. The Henley style round neckline includes a three-button placket.", "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg", "Mens Casual Premium Slim Fit T-Shirts", 22.3m, 259, null },
                    { 3L, 1L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4203), "great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.", "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg", "Mens Cotton Jacket", 55.99m, 500, null },
                    { 4L, 1L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4206), "The color could be slightly different between on the screen and in practice. / Please note that body builds vary by person, therefore, detailed size information should be reviewed below on the product description.", "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_.jpg", "Mens Casual Slim Fit", 15.99m, 430, null },
                    { 5L, 3L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4209), "From our Legends Collection, the Naga was inspired by the mythical water dragon that protects the ocean's pearl. Wear facing inward to be bestowed with love and abundance, or outward for protection.", "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_.jpg", "John Hardy Women's Legends Naga Gold & Silver Dragon Station Chain Bracelet", 695.00m, 400, null },
                    { 6L, 3L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4227), "Satisfaction Guaranteed. Return or exchange any order within 30 days.Designed and sold by Hafeez Center in the United States. Satisfaction Guaranteed. Return or exchange any order within 30 days.", "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_.jpg", "Solid Gold Petite Micropave ", 168.00m, 70, null },
                    { 7L, 3L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4229), "Classic Created Wedding Engagement Solitaire Diamond Promise Ring for Her. Gifts to spoil your love more for Engagement, Wedding, Anniversary, Valentine's Day...", "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_.jpg", "White Gold Plated Princess", 9.99m, 400, null },
                    { 8L, 3L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4231), "Rose Gold Plated Double Flared Tunnel Plug Earrings. Made of 316L Stainless Steel", "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_.jpg", "Pierced Owl Rose Gold Plated Stainless Steel Double", 10.99m, 100, null },
                    { 9L, 4L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4233), "USB 3.0 and USB 2.0 Compatibility Fast data transfers Improve PC Performance High Capacity; Compatibility Formatted NTFS for Windows 10, Windows 8.1, Windows 7; Reformatting may be required for other operating systems; Compatibility may vary depending on user’s hardware configuration and operating system", "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_.jpg", "WD 2TB Elements Portable External Hard Drive - USB 3.0 ", 64.00m, 203, null },
                    { 10L, 4L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4237), "Easy upgrade for faster boot up, shutdown, application load and response (As compared to 5400 RPM SATA 2.5” hard drive; Based on published specifications and internal benchmarking tests using PCMark vantage scores) Boosts burst write performance, making it ideal for typical PC workloads The perfect balance of performance and reliability Read/write speeds of up to 535MB/s/450MB/s (Based on internal testing; Performance may vary depending upon drive capacity, host device, OS and application.)", "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_.jpg", "SanDisk SSD PLUS 1TB Internal SSD - SATA III 6 Gb/s", 109.00m, 470, null },
                    { 11L, 4L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4239), "3D NAND flash are applied to deliver high transfer speeds Remarkable transfer speeds that enable faster bootup and improved overall system performance. The advanced SLC Cache Technology allows performance boost and longer lifespan 7mm slim design suitable for Ultrabooks and Ultra-slim notebooks. Supports TRIM command, Garbage Collection technology, RAID, and ECC (Error Checking & Correction) to provide the optimized performance and enhanced reliability.", "https://fakestoreapi.com/img/71kWymZ+c+L._AC_SX679_.jpg", "Silicon Power 256GB SSD 3D NAND A55 SLC Cache Performance Boost SATA III 2.5", 109.00m, 319, null },
                    { 12L, 4L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4241), "Expand your PS4 gaming experience, Play anywhere Fast and easy, setup Sleek design with high capacity, 3-year manufacturer's limited warranty", "https://fakestoreapi.com/img/61mtL65D4cL._AC_SX679_.jpg", "WD 4TB Gaming Drive Works with Playstation 4 Portable External Hard Drive", 114.00m, 400, null },
                    { 13L, 4L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4244), "21. 5 inches Full HD (1920 x 1080) widescreen IPS display And Radeon free Sync technology. No compatibility for VESA Mount Refresh Rate: 75Hz - Using HDMI port Zero-frame design | ultra-thin | 4ms response time | IPS panel Aspect ratio - 16: 9. Color Supported - 16. 7 million colors. Brightness - 250 nit Tilt angle -5 degree to 15 degree. Horizontal viewing angle-178 degree. Vertical viewing angle-178 degree 75 hertz", "https://fakestoreapi.com/img/81QpkIctqPL._AC_SX679_.jpg", "Acer SB220Q bi 21.5 inches Full HD (1920 x 1080) IPS Ultra-Thin", 599.00m, 250, null },
                    { 14L, 4L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4246), "49 INCH SUPER ULTRAWIDE 32:9 CURVED GAMING MONITOR with dual 27 inch screen side by side QUANTUM DOT (QLED) TECHNOLOGY, HDR support and factory calibration provides stunningly realistic and accurate color and contrast 144HZ HIGH REFRESH RATE and 1ms ultra fast response time work to eliminate motion blur, ghosting, and reduce input lag", "https://fakestoreapi.com/img/81Zt42ioCgL._AC_SX679_.jpg", "Samsung 49-Inch CHG90 144Hz Curved Gaming Monitor (LC49HG90DMNXZA) – Super Ultrawide Screen QLED ", 999.99m, 140, null },
                    { 15L, 2L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4248), "Note:The Jackets is US standard size, Please choose size as your usual wear Material: 100% Polyester; Detachable Liner Fabric: Warm Fleece. Detachable Functional Liner: Skin Friendly, Lightweigt and Warm.Stand Collar Liner jacket, keep you warm in cold weather. Zippered Pockets: 2 Zippered Hand Pockets, 2 Zippered Pockets on Chest (enough to keep cards or keys)and 1 Hidden Pocket Inside.Zippered Hand Pockets and Hidden Pocket keep your things secure. Humanized Design: Adjustable and Detachable Hood and Adjustable cuff to prevent the wind and water,for a comfortable fit. 3 in 1 Detachable Design provide more convenience, you can separate the coat and inner as needed, or wear it together. It is suitable for different season and help you adapt to different climates", "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_.jpg", "BIYLACLESEN Women's 3-in-1 Snowboard Jacket Winter Coats", 56.99m, 235, null },
                    { 16L, 2L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4250), "100% POLYURETHANE(shell) 100% POLYESTER(lining) 75% POLYESTER 25% COTTON (SWEATER), Faux leather material for style and comfort / 2 pockets of front, 2-For-One Hooded denim style faux leather jacket, Button detail on waist / Detail stitching at sides, HAND WASH ONLY / DO NOT BLEACH / LINE DRY / DO NOT IRON", "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_.jpg", "Lock and Love Women's Removable Hooded Faux Leather Moto Biker Jacket", 29.95m, 340, null },
                    { 17L, 2L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4252), "Lightweight perfet for trip or casual wear---Long sleeve with hooded, adjustable drawstring waist design. Button and zipper front closure raincoat, fully stripes Lined and The Raincoat has 2 side pockets are a good size to hold all kinds of things, it covers the hips, and the hood is generous but doesn't overdo it.Attached Cotton Lined Hood with Adjustable Drawstrings give it a real styled look.", "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2.jpg", "Rain Jacket Women Windbreaker Striped Climbing Raincoats", 39.99m, 679, null },
                    { 18L, 2L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4256), "95% RAYON 5% SPANDEX, Made in USA or Imported, Do Not Bleach, Lightweight fabric with great stretch for comfort, Ribbed on sleeves and neckline / Double stitching on bottom hem", "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_.jpg", "MBJ Women's Solid Short Sleeve Boat Neck V ", 9.85m, 130, null },
                    { 19L, 2L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4258), "100% Polyester, Machine wash, 100% cationic polyester interlock, Machine Wash & Pre Shrunk for a Great Fit, Lightweight, roomy and highly breathable with moisture wicking fabric which helps to keep moisture away, Soft Lightweight Fabric with comfortable V-neck collar and a slimmer fit, delivers a sleek, more feminine silhouette and Added Comfort", "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_.jpg", "Opna Women's Short Sleeve Moisture", 7.95m, 146, null },
                    { 20L, 2L, new DateTime(2024, 12, 5, 16, 20, 29, 237, DateTimeKind.Utc).AddTicks(4261), "95%Cotton,5%Spandex, Features: Casual, Short Sleeve, Letter Print,V-Neck,Fashion Tees, The fabric is soft and has some stretch., Occasion: Casual/Office/Beach/School/Home/Street. Season: Spring,Summer,Autumn,Winter.", "https://fakestoreapi.com/img/61pHAEJ4NML._AC_UX679_.jpg", "DANVOUY Womens T Shirt Casual Cotton Short", 12.99m, 145, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20L);

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
                column: "CreatedDate",
                value: new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 4, 22, 18, 36, 89, DateTimeKind.Utc).AddTicks(8788));
        }
    }
}
