using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class DishCategoriesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "dish_categories",
                columns: new[] { "id", "title" },
                values: new object[,]
                {
                    { 1L, "Picos" },
                    { 2L, "Gerimai" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "dish_categories",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "dish_categories",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
