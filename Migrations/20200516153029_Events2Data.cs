using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class Events2Data : Migration
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

            migrationBuilder.UpdateData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 1L,
                column: "name",
                value: "Saskaita");

            migrationBuilder.UpdateData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Atsaukimas");

            migrationBuilder.UpdateData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 3L,
                column: "name",
                value: "Klientas");

            migrationBuilder.UpdateData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 4L,
                column: "name",
                value: "Uzsakymas");

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
                table: "event_type",
                keyColumn: "id",
                keyValue: 1L,
                column: "name",
                value: "Saskaitos prasymas");

            migrationBuilder.UpdateData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Uzsakymas atsauktas");

            migrationBuilder.UpdateData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 3L,
                column: "name",
                value: "Naujas klientas");

            migrationBuilder.UpdateData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 4L,
                column: "name",
                value: "Naujas uzsakymas");

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
