using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class Menu_Food_Data : Migration
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
                table: "menu_dishes",
                columns: new[] { "fk_dishes", "fk_menus", "date_from", "date_until" },
                values: new object[,]
                {
                    { 1L, 1L, null, null },
                    { 2L, 1L, null, null },
                    { 3L, 1L, null, null },
                    { 1L, 2L, null, null },
                    { 2L, 2L, null, null }
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
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 1L, 2L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 2L, 1L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 3L, 1L });

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
