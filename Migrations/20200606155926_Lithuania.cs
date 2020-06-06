using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class Lithuania : Migration
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

            migrationBuilder.UpdateData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "name", "surname" },
                values: new object[] { "Jonas", "Jonaitis" });

            migrationBuilder.UpdateData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "name", "surname" },
                values: new object[] { "Jozefina", "Jozefinaite" });

            migrationBuilder.UpdateData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "name", "surname" },
                values: new object[] { "Petras", "Petraitis" });


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

            migrationBuilder.UpdateData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "name", "surname" },
                values: new object[] { "User1", "User1" });

            migrationBuilder.UpdateData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "name", "surname" },
                values: new object[] { "User2", "User2" });

            migrationBuilder.UpdateData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "name", "surname" },
                values: new object[] { "User3", "User3" });

            migrationBuilder.UpdateData(
                table: "user_role",
                keyColumn: "id",
                keyValue: 1L,
                column: "name",
                value: "administrator");

            migrationBuilder.UpdateData(
                table: "user_role",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "waiter");
        }
    }
}
