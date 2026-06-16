using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 7, "The Ordinary", 3, "Serum za nepravilnosti kože", "https://theordinary.com/on/demandware.static/-/Sites-deciem-master-catalog/default/dwf7fbba01/Images/products/The%20Ordinary/rdn-niacinamide-10pct-zinc-1pct-30ml.png", "Niacinamide Serum", 1350m, 25 },
                    { 8, "La Roche-Posay", 1, "Krema za problematičnu kožu", "https://www.laroche-posay.rs/-/media/project/loreal/brand-sites/lrp/emea/rs/products/effaclar/effaclar-duo-plus/effaclar-duo-plus-40ml.png", "Effaclar Duo+", 2200m, 18 },
                    { 9, "Dior", 4, "Luksuzni ruž za usne", "https://www.dior.com/on/demandware.static/-/Sites-masterCatalog_Dior/default/dwff5c3f15/Y0286000/Y0286000_C028600999_E01_ZHC.jpg", "Rouge Dior Lipstick", 5900m, 9 },
                    { 10, "YSL", 5, "Intenzivan parfem", "https://www.yslbeautyus.com/dw/image/v2/AANG_PRD/on/demandware.static/-/Sites-ysl-master-catalog/default/dw089a49ce/2022/Fragrance/Black%20Opium/3365440787971_black_opium_eau_de_parfum_50ml_alt1.jpg", "Black Opium", 13200m, 7 },
                    { 11, "Bioderma", 1, "Micelarna voda", "https://www.bioderma.us/sites/us/files/products/sensibio-h2o-500ml.png", "Micellar Water", 1750m, 30 },
                    { 12, "Maybelline", 4, "Tečni puder", "https://www.maybelline.com/~/media/mny/global/face-makeup/foundation/fit-me-matte-poreless-foundation/fit-me-matte-poreless-foundation-120-classic-ivory-o.jpg", "Fit Me Foundation", 1200m, 22 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
