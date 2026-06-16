using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class EnglishCategoriesAndProductsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Cleansers, creams, serums and daily face care products", "Skincare" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Body lotions, shower gels, oils and body care essentials", "Body Care" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Shampoos, conditioners, masks and hair treatments", "Hair Care" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Foundation, lipstick, mascara and decorative cosmetics", "Makeup" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Perfumes and signature scents", "Fragrance" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 101, "CeraVe", 1, "Gentle hydrating facial cleanser", "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/hydrating-cleanser/hydrating-cleanser-front-lg.jpg", "Hydrating Cleanser", 1550m, 20 },
                    { 102, "CeraVe", 1, "Rich moisturizer for dry skin", "https://static.beautytocare.com/cdn-cgi/image/f=auto/media/catalog/product/c/e/cerave-moisturizing-cream-dry-to-very-dry-skin-170g_3.jpg", "Moisturizing Cream", 1800m, 15 },
                    { 103, "The Ordinary", 1, "Serum for blemish-prone skin", "https://metropoliten.rs/upload/catalog/variation/3304/04416_1_66e968f5b08d3.jpg", "Niacinamide Serum", 1350m, 25 },
                    { 104, "La Roche-Posay", 1, "Corrective care for problematic skin", "https://www.laroche-posay.rs/-/media/project/loreal/brand-sites/lrp/emea/adba/products/effaclar/effaclar-duo-plus-m/larocheposayproductacneeffaclarduom15ml337875863841fws.png", "Effaclar Duo+", 2200m, 18 },
                    { 105, "Neutrogena", 2, "Deeply hydrating body lotion", "https://m.media-amazon.com/images/I/61nfk4gYpLL._AC_UF1000,1000_QL80_.jpg", "Body Lotion", 1450m, 16 },
                    { 106, "Bioderma", 2, "Gentle shower oil for sensitive skin", "https://www.bioderma.co.uk/sites/uk/files/products/atoderm-huile-de-douche-1l.png", "Shower Oil", 1900m, 14 },
                    { 107, "Tree Hut", 2, "Exfoliating sugar body scrub", "https://m.media-amazon.com/images/I/81PnO6sE6RL.jpg", "Body Scrub", 2100m, 12 },
                    { 108, "Nuxe", 2, "Multi-purpose dry oil for body and hair", "https://www.nuxe.com/cdn/shop/files/huile-prodigieuse-100ml.png", "Body Oil", 3900m, 10 },
                    { 109, "L'Oréal Paris", 3, "Repairing shampoo for damaged hair", "https://m.media-amazon.com/images/I/61S2Tn63SLL.jpg", "Elvive Shampoo", 850m, 30 },
                    { 110, "Garnier", 3, "Nourishing mask for dry hair", "https://m.media-amazon.com/images/I/71JzQJ+7dfL.jpg", "Hair Food Banana Mask", 1150m, 22 },
                    { 111, "Olaplex", 3, "Lightweight oil for shine and repair", "https://m.media-amazon.com/images/I/61-Q1eFS1RL.jpg", "Bonding Oil", 4200m, 9 },
                    { 112, "Kérastase", 3, "Anti-breakage conditioner for weakened hair", "https://m.media-amazon.com/images/I/51xTck41NXL.jpg", "Genesis Conditioner", 3600m, 11 },
                    { 113, "Estée Lauder", 4, "Long-wear liquid foundation", "https://www.odkshop.com/cdn/shop/files/rgC1jadriv_600x.jpg?v=1726613741", "Double Wear Foundation", 6500m, 12 },
                    { 114, "Dior", 4, "Luxury satin lipstick", "https://apothecary.rs/27333-large_default/dior-rouge-dior-lipstick-n434-satin-finish-promenade-promenade-a-bold-rosewood-35g.jpg", "Rouge Dior Lipstick", 5900m, 9 },
                    { 115, "Dior", 4, "Glossy lip oil with nourishing finish", "https://apothecary.rs/18361-large_default/dior-lip-glow-oil-n001-pink-6ml.jpg", "Lip Glow Oil", 5200m, 10 },
                    { 116, "Maybelline", 4, "Natural finish liquid foundation", "https://www.apotekajankovic.rs/image/cache/wp/lj/Ivory.webp", "Fit Me Foundation", 1200m, 22 },
                    { 117, "Rare Beauty", 4, "Liquid blush with soft glowing finish", "https://www.sephora.com/productimages/sku/s2569598-main-zoom.jpg", "Rare Beauty Blush", 4200m, 14 },
                    { 118, "Dior", 5, "Elegant floral fragrance", "https://m.media-amazon.com/images/I/61R1+HpKFPL._AC_UF1000,1000_QL80_.jpg", "Miss Dior Parfum", 14500m, 8 },
                    { 119, "YSL", 5, "Warm and intense evening fragrance", "https://jasmin.b-cdn.net/media/catalog/product/cache/4456161891bc26600241f10587ca424f/3/3/3365440787858-10ff7fd8-590b-4914-af76-6220761f0543_1.jpg", "Black Opium", 13200m, 7 },
                    { 120, "YSL", 5, "Bold floral lavender fragrance", "https://m.media-amazon.com/images/I/61hU8lTRy-L._AC_UF1000,1000_QL80_.jpg", "Libre Eau de Parfum", 13900m, 6 },
                    { 121, "Carolina Herrera", 5, "Iconic sweet and elegant fragrance", "https://m.media-amazon.com/images/I/61VFB+M2B6L._AC_UF1000,1000_QL80_.jpg", "Good Girl", 12800m, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Proizvodi za čišćenje, hidrataciju i negu lica", "Nega lica" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Balzami, ulja i tretmani za negu usana", "Nega usana" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Serumi za hidrataciju, obnovu i negu kože", "Serumi" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Dekorativna kozmetika i makeup proizvodi", "Šminka" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Parfemi i mirisi", "Parfemi" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "CeraVe", 1, "Gel za čišćenje lica", "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/hydrating-cleanser/hydrating-cleanser-front-lg.jpg", "Hydrating Cleanser", 1550m, 20 },
                    { 2, "Dior", 2, "Ulje za usne", "https://apothecary.rs/18361-large_default/dior-lip-glow-oil-n001-pink-6ml.jpg", "Lip Glow Oil", 5200m, 10 },
                    { 3, "Estée Lauder", 3, "Serum za lice", "https://jasmin.b-cdn.net/media/catalog/product/cache/4456161891bc26600241f10587ca424f/0/8/0887167485488_8.jpg", "Advanced Night Repair", 11800m, 5 },
                    { 4, "CeraVe", 1, "Krema za suvu kožu", "https://static.beautytocare.com/cdn-cgi/image/f=auto/media/catalog/product/c/e/cerave-moisturizing-cream-dry-to-very-dry-skin-170g_3.jpg", "Moisturizing Cream", 1800m, 15 },
                    { 5, "Dior", 5, "Cvetni parfem", "https://m.media-amazon.com/images/I/61R1+HpKFPL._AC_UF1000,1000_QL80_.jpg", "Miss Dior Parfum", 14500m, 8 },
                    { 6, "Estée Lauder", 4, "Dugotrajni puder", "https://www.odkshop.com/cdn/shop/files/rgC1jadriv_600x.jpg?v=1726613741", "Double Wear Foundation", 6500m, 12 },
                    { 7, "The Ordinary", 3, "Serum za nepravilnosti kože", "https://metropoliten.rs/upload/catalog/variation/3304/04416_1_66e968f5b08d3.jpg", "Niacinamide Serum", 1350m, 25 },
                    { 8, "La Roche-Posay", 1, "Krema za problematičnu kožu", "https://www.laroche-posay.rs/-/media/project/loreal/brand-sites/lrp/emea/adba/products/effaclar/effaclar-duo-plus-m/larocheposayproductacneeffaclarduom15ml337875863841fws.png", "Effaclar Duo+", 2200m, 18 },
                    { 9, "Dior", 4, "Luksuzni ruž za usne", "https://apothecary.rs/27333-large_default/dior-rouge-dior-lipstick-n434-satin-finish-promenade-promenade-a-bold-rosewood-35g.jpg", "Rouge Dior Lipstick", 5900m, 9 },
                    { 10, "YSL", 5, "Intenzivan parfem", "https://jasmin.b-cdn.net/media/catalog/product/cache/4456161891bc26600241f10587ca424f/3/3/3365440787858-10ff7fd8-590b-4914-af76-6220761f0543_1.jpg", "Black Opium", 13200m, 7 },
                    { 11, "Bioderma", 1, "Micelarna voda", "https://benu.rs/data/images/product-640x640-webp/6/4_38746.webp", "Micellar Water", 1750m, 30 },
                    { 12, "Maybelline", 4, "Tečni puder", "https://www.apotekajankovic.rs/image/cache/wp/lj/Ivory.webp", "Fit Me Foundation", 1200m, 22 }
                });
        }
    }
}
