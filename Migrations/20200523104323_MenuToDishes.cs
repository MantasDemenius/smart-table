using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class MenuToDishes : Migration
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
                    { 4L, 3L, null, null },
                    { 5L, 3L, null, null },
                    { 6L, 3L, null, null },
                    { 7L, 3L, null, null },
                    { 8L, 3L, null, null },
                    { 9L, 3L, null, null },
                    { 10L, 3L, null, null },
                    { 11L, 3L, null, null },
                    { 12L, 3L, null, null },
                    { 13L, 3L, null, null },
                    { 14L, 3L, null, null }
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

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 3L,
                column: "date_time",
                value: new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 4L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 5L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 6L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 7L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 8L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 9L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 10L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 11L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 12L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 13L, 3L });

            migrationBuilder.DeleteData(
                table: "menu_dishes",
                keyColumns: new[] { "fk_dishes", "fk_menus" },
                keyValues: new object[] { 14L, 3L });

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

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 3L,
                column: "date_time",
                value: new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
