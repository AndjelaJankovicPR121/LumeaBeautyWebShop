using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UnapredjenModelProizvoda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/hydrating-cleanser/hydrating-cleanser-front-lg.jpg");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 4, "CeraVe", "Krema za suvu kožu", "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/moisturising-cream/moisturising-cream-v2.jpg", "Moisturizing Cream", 1800m, 15 },
                    { 5, "Dior", "Cvetni parfem", "https://jasmin.b-cdn.net/media/catalog/product/3/3/3348901502421_1.jpg", "Miss Dior Parfum", 14500m, 8 },
                    { 6, "Estée Lauder", "Dugotrajni puder", "https://jasmin.b-cdn.net/media/catalog/product/0/0/0027131179340_1.jpg", "Double Wear Foundation", 6500m, 12 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/hydrating-cleanser/hydrating-cleanser-front-lg.jpg?rev=-1");
        }
    }
}
