using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class DishesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "calories", "description", "discount", "fk_dish_categories", "price", "title" },
                values: new object[,]
                {
                    { 1L, 300, "Labai skani pica", 20.0, 1L, 11.99, "Capriciosa" },
                    { 2L, -1, "Nesveika, bet skanu", 0.0, 2L, 1.99, "Cola" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
