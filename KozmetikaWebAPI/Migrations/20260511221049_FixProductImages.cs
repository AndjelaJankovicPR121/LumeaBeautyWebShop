using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 105,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 106,
                column: "ImageUrl",
                value: "https://www.facethefuture.co.uk/cdn/shop/files/7B187929_7D__7B_7D__7B28");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 107,
                column: "ImageUrl",
                value: "https://www.treehutshea.com/cdn/shop/products/peach-sorbet-front.jpg?v=1675379991");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 108,
                column: "ImageUrl",
                value: "https://us.nuxe.com/cdn/shop/files/3264680009761-0A38031-VIEW_2-HP-CLASSIQUE_50ML-PACKSHOT_2000x2000_d65cba9d-0608-4b48-a48f-0eeccea6f94e.jpg?crop=center&height=1200&v=1718290583&width=1200");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 109,
                column: "ImageUrl",
                value: "https://enemmall.com/cdn/shop/files/3312669.jpg?v=1757331037");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 110,
                column: "ImageUrl",
                value: "https://main.thgimages.com/?url=https://static.thcdn.com/productimg/original/12030813-1485131248642276.jpg&format=webp&width=1500&height=1500&fit=cover");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 111,
                column: "ImageUrl",
                value: "https://www.prodajakozmetike.com/image/data/Revlon%20SM/olaplex-no-7-bonding-oil-30ml-2821.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 112,
                column: "ImageUrl",
                value: "https://numi.rs/wp-content/uploads/2024/09/Kerastase-Genesis-Fondant-Renforcateur-Conditioner-200ml.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 120,
                column: "ImageUrl",
                value: "https://www.parfimerijatajna.rs/files/2022/11/3614273776127_1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 121,
                column: "ImageUrl",
                value: "https://parfembox.rs/wp-content/uploads/2020/06/carolina_herrera_good_girl_50_ml.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 105,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/61nfk4gYpLL._AC_UF1000,1000_QL80_.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 106,
                column: "ImageUrl",
                value: "https://www.bioderma.co.uk/sites/uk/files/products/atoderm-huile-de-douche-1l.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 107,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/81PnO6sE6RL.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 108,
                column: "ImageUrl",
                value: "https://www.nuxe.com/cdn/shop/files/huile-prodigieuse-100ml.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 109,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/61S2Tn63SLL.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 110,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/71JzQJ+7dfL.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 111,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/61-Q1eFS1RL.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 112,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/51xTck41NXL.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 120,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/61hU8lTRy-L._AC_UF1000,1000_QL80_.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 121,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/61VFB+M2B6L._AC_UF1000,1000_QL80_.jpg");
        }
    }
}
