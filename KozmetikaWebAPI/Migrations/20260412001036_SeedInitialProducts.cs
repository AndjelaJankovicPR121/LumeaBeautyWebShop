using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "CeraVe", "Gel za čišćenje lica", "https://images.heb.com/is/image/HEBGrocery/002123533", "Hydrating Cleanser", 1550m, 20 },
                    { 2, "Dior", "Ulje za usne", "https://www.dior.com/dw/image/v2/BGST_PRD/on/demandware.static/-/Sites-masterCatalog_Dior/default/dw4f2b1d3a/assets/Y0124000/Y0124000_C012400001_E01.jpg", "Lip Glow Oil", 5200m, 10 },
                    { 3, "Estée Lauder", "Serum za lice", "https://www.esteelauder.com/media/export/cms/products/640x600/el_sku_PG6501_640x600_0.jpg", "Advanced Night Repair", 11800m, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
