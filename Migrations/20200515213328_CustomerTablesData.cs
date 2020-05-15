using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class CustomerTablesData : Migration
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
                table: "customer_tables",
                columns: new[] { "id", "is_taken", "join_code", "qr_code", "seats_count" },
                values: new object[,]
                {
                    { 1L, true, "DEF", "ABC", 6 },
                    { 2L, false, "wxz", "qrt", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer_tables",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "customer_tables",
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
        }
    }
}
