using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class v2_EnumValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "event_type",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "bill_request" },
                    { 2L, "cancel_order" },
                    { 3L, "new_customer" },
                    { 4L, "new_order" }
                });

            migrationBuilder.InsertData(
                table: "user_role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "administrator" },
                    { 2L, "waiter" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "event_type",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "user_role",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "user_role",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
