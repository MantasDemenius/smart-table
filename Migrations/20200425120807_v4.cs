using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 1,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 2,
                column: "Role",
                value: 2);

            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 3,
                column: "Role",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 1,
                column: "Role",
                value: 0);

            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 2,
                column: "Role",
                value: 1);

            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 3,
                column: "Role",
                value: 1);
        }
    }
}
