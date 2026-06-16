using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/hydrating-cleanser/hydrating-cleanser-front-lg.jpg?rev=-1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://apothecary.rs/18361-large_default/dior-lip-glow-oil-n001-pink-6ml.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://jasmin.b-cdn.net/media/catalog/product/cache/4456161891bc26600241f10587ca424f/0/8/0887167485488_8.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://images.heb.com/is/image/HEBGrocery/002123533");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.dior.com/dw/image/v2/BGST_PRD/on/demandware.static/-/Sites-masterCatalog_Dior/default/dw4f2b1d3a/assets/Y0124000/Y0124000_C012400001_E01.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.esteelauder.com/media/export/cms/products/640x600/el_sku_PG6501_640x600_0.jpg");
        }
    }
}
