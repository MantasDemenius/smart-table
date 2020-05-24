using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 4L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 5L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

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

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 4L,
                column: "date_time",
                value: new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 4L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 5L,
                columns: new[] { "date_time", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

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

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 4L,
                column: "date_time",
                value: new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
