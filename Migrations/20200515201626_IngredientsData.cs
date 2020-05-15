using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class IngredientsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ingredients",
                columns: new[] { "id", "title" },
                values: new object[,]
                {
                    { 1L, "Kumpis" },
                    { 2L, "Suris" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
