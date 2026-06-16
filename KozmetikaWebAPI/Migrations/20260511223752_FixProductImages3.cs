using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixProductImages3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 117,
                column: "ImageUrl",
                value: "https://www.spacenk.com/on/demandware.static/-/Sites-spacenkmastercatalog/default/dw95036ea5/products/RARE/UK200048738_RARE.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 117,
                column: "ImageUrl",
                value: "https://www.sephora.com/productimages/sku/s2569598-main-zoom.jpg");
        }
    }
}
