using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class BiggerMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date_time",
                table: "orders",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_DATE");

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
                values: new object[,]
                {
                    { 14L, 0, "500ml", 15.0, 2L, 1.5, "Vanduo" },
                    { 13L, 20, "330ml", 50.0, 2L, 1.6499999999999999, "Gira" },
                    { 11L, 300, "330ml", 0.0, 2L, 1.75, "Pepsi" },
                    { 10L, 20, "Skirtingu skoniu.", 0.0, 2L, 1.45, "JD naminis limonadas" },
                    { 12L, 0, "330ml", 0.0, 2L, 1.75, "Pepsi MAX" },
                    { 8L, 200, "Naminis pomidoru padazas - letai kepta plesyta kiauliena - musu gamintas kepsniu padazas", 0.0, 1L, 10.5, "TEXAS COWBOY" },
                    { 7L, 1520, "Trinti pomidorai - skamorca - mocarela - pekorino suris - peperoni desra - kepsniu padazas", 30.0, 1L, 12.0, "MESOS OMG!" },
                    { 6L, 400, "Pomidoru padazas - peperoni desra - pankoliu ir anyziumi gardintos desreles - mocarela - marinuoti raudonieji", 0.0, 1L, 11.0, "BIG TONY'S" },
                    { 5L, 1500, "Pomidoru padazas, malta peperoni desra, kepti cesnakai, karamelizuoti svogunai, mocarela, grazgarstes", 20.0, 1L, 9.0, "The Wall Street" },
                    { 9L, 2000, "Pomidoru padazas - marinuota kepta vistiena - musu gamintas kepsniu padazas - parmezanas", 50.0, 1L, 10.5, "FRIKIN CHICKEN" },
                    { 4L, 2000, "Pomidoru padazas, raudonieji svogunai, letai kepta plesyta jautiena, marinuoti agurkeliai, cederis", 0.0, 1L, 16.0, "Yankee Burger" }
                });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "id", "date_from", "date_until", "fri", "is_active", "mon", "sat", "sun", "thu", "time_from", "time_until", "title", "tue", "wed" },
                values: new object[] { 3L, null, null, true, true, true, true, true, true, null, null, "Brooklyn Brothers", true, true });

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
                table: "dishes",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "dishes",
                keyColumn: "id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_time",
                table: "orders",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

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
