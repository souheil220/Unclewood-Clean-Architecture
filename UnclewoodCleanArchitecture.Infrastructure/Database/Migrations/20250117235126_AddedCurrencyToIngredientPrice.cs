using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnclewoodCleanArchitecture.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedCurrencyToIngredientPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price_Currency",
                table: "Ingredients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_Currency",
                table: "Ingredients");
        }
    }
}
