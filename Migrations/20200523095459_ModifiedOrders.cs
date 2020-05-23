using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class ModifiedOrders : Migration
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
                table: "orders",
                columns: new[] { "id", "date_time", "fk_bills", "fk_registered_users", "served", "submitted", "temperature" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 2L, true, true, 15.0 },
                    { 2L, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 3L, false, true, 17.0 },
                    { 3L, new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, true, 19.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "id",
                keyValue: 3L);

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
