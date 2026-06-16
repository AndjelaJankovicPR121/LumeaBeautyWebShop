using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixProductImages1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 106,
                column: "ImageUrl",
                value: "https://apoteka-apolo.rs/wp-content/uploads/2024/08/37735-Bioderma-atoderm-ulje-za-tusiranje-1l-640x640-1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 111,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/71ACqtMWo2L._AC_UF1000,1000_QL80_.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 106,
                column: "ImageUrl",
                value: "https://www.facethefuture.co.uk/cdn/shop/files/7B187929_7D__7B_7D__7B28");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 111,
                column: "ImageUrl",
                value: "https://www.prodajakozmetike.com/image/data/Revlon%20SM/olaplex-no-7-bonding-oil-30ml-2821.jpg");
        }
    }
}
