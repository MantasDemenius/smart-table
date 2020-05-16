using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class Menu_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 1L,
                column: "date_time",
                value: new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 2L,
                column: "date_time",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "id", "calories", "description", "discount", "fk_dish_categories", "price", "title" },
                values: new object[] { 3L, 20, "Þalioji arbata, rytinë", 0.0, 2L, 1.0, "Arbata" });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "date_from", "date_until", "fri", "is_active", "mon", "sat", "sun", "thu", "time_from", "time_until", "title", "tue", "wed" },
                values: new object[,]
                {
                    { 1L, null, null, true, true, true, true, true, true, new TimeSpan(0, 6, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "Pusryciu meniu", true, true },
                    { 2L, null, null, true, true, true, true, true, true, null, null, "Pagrindinis meniu", true, true }
                });

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 1L,
                column: "date_time",
                value: new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 2L,
                column: "date_time",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 1L,
                column: "date_time",
                value: new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 2L,
                column: "date_time",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 1L,
                column: "date_time",
                value: new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 2L,
                column: "date_time",
                value: new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
