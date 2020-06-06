using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class RoleIncrease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "user_role",
                fixedLength: true,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(13)",
                oldFixedLength: true,
                oldMaxLength: 13);

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
    table: "user_role",
    keyColumn: "id",
    keyValue: 1L,
    column: "name",
    value: "Administratorius");

            migrationBuilder.UpdateData(
                table: "user_role",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Padavejas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "user_role",
                type: "character(13)",
                fixedLength: true,
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 255);

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
