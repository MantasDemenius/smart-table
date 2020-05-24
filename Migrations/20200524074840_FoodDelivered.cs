using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class FoodDelivered : Migration
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
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 4L,
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 5L,
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 0.0 });

            migrationBuilder.UpdateData(
                table: "customer_tables",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "is_taken", "qr_code" },
                values: new object[] { false, "http://localhost:65312/TakeTable?id=1" });

            migrationBuilder.UpdateData(
                table: "customer_tables",
                keyColumn: "id",
                keyValue: 2L,
                column: "qr_code",
                value: "http://localhost:65312/TakeTable?id=2");

            migrationBuilder.UpdateData(
                table: "customer_tables",
                keyColumn: "id",
                keyValue: 3L,
                column: "qr_code",
                value: "http://localhost:65312/TakeTable?id=3");

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
                columns: new[] { "date_time", "served" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true });

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "date_time", "served" },
                values: new object[] { new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), true });

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 4L,
                columns: new[] { "date_time", "served" },
                values: new object[] { new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), true });
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
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 4L,
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0.0 });

            migrationBuilder.UpdateData(
                table: "bills",
                keyColumn: "id",
                keyValue: 5L,
                columns: new[] { "date_time", "is_paid", "tips" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0.0 });

            migrationBuilder.UpdateData(
                table: "customer_tables",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "is_taken", "qr_code" },
                values: new object[] { true, "http://localhost:65312/QrCode/1" });

            migrationBuilder.UpdateData(
                table: "customer_tables",
                keyColumn: "id",
                keyValue: 2L,
                column: "qr_code",
                value: "http://localhost:65312/QrCode/2");

            migrationBuilder.UpdateData(
                table: "customer_tables",
                keyColumn: "id",
                keyValue: 3L,
                column: "qr_code",
                value: "http://localhost:65312/QrCode/3");

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
                columns: new[] { "date_time", "served" },
                values: new object[] { new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "date_time", "served" },
                values: new object[] { new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "id",
                keyValue: 4L,
                columns: new[] { "date_time", "served" },
                values: new object[] { new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }
    }
}
