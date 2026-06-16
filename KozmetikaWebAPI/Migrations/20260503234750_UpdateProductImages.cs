using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://static.beautytocare.com/cdn-cgi/image/f=auto/media/catalog/product/c/e/cerave-moisturizing-cream-dry-to-very-dry-skin-170g_3.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/61R1+HpKFPL._AC_UF1000,1000_QL80_.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://www.odkshop.com/cdn/shop/files/rgC1jadriv_600x.jpg?v=1726613741");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://metropoliten.rs/upload/catalog/variation/3304/04416_1_66e968f5b08d3.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://www.laroche-posay.rs/-/media/project/loreal/brand-sites/lrp/emea/adba/products/effaclar/effaclar-duo-plus-m/larocheposayproductacneeffaclarduom15ml337875863841fws.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://apothecary.rs/27333-large_default/dior-rouge-dior-lipstick-n434-satin-finish-promenade-promenade-a-bold-rosewood-35g.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://jasmin.b-cdn.net/media/catalog/product/cache/4456161891bc26600241f10587ca424f/3/3/3365440787858-10ff7fd8-590b-4914-af76-6220761f0543_1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://benu.rs/data/images/product-640x640-webp/6/4_38746.webp");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://www.apotekajankovic.rs/image/cache/wp/lj/Ivory.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/moisturising-cream/moisturising-cream-v2.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://jasmin.b-cdn.net/media/catalog/product/3/3/3348901502421_1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://jasmin.b-cdn.net/media/catalog/product/0/0/0027131179340_1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://theordinary.com/on/demandware.static/-/Sites-deciem-master-catalog/default/dwf7fbba01/Images/products/The%20Ordinary/rdn-niacinamide-10pct-zinc-1pct-30ml.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://www.laroche-posay.rs/-/media/project/loreal/brand-sites/lrp/emea/rs/products/effaclar/effaclar-duo-plus/effaclar-duo-plus-40ml.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://www.dior.com/on/demandware.static/-/Sites-masterCatalog_Dior/default/dwff5c3f15/Y0286000/Y0286000_C028600999_E01_ZHC.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://www.yslbeautyus.com/dw/image/v2/AANG_PRD/on/demandware.static/-/Sites-ysl-master-catalog/default/dw089a49ce/2022/Fragrance/Black%20Opium/3365440787971_black_opium_eau_de_parfum_50ml_alt1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://www.bioderma.us/sites/us/files/products/sensibio-h2o-500ml.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://www.maybelline.com/~/media/mny/global/face-makeup/foundation/fit-me-matte-poreless-foundation/fit-me-matte-poreless-foundation-120-classic-ivory-o.jpg");
        }
    }
}
