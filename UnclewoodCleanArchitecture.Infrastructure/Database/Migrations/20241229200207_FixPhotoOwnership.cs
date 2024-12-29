using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnclewoodCleanArchitecture.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixPhotoOwnership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Meals_MealId1",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_MealId1",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "MealId1",
                table: "Photo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MealId1",
                table: "Photo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Photo_MealId1",
                table: "Photo",
                column: "MealId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Meals_MealId1",
                table: "Photo",
                column: "MealId1",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
