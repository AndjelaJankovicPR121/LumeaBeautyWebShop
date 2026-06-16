using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KozmetikaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixProductImages2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 105,
                column: "ImageUrl",
                value: "https://images.ctfassets.net/b7vjv6cc1lvj/2ZDEKfi7jzTB2dalCyiJOG/3fd420402e8142cba22e16ab5ea04cd2/grlc7-en-gb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 105,
                column: "ImageUrl",
                value: "");
        }
    }
}
