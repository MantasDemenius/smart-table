using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 1,
                column: "Role",
                value: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 1,
                column: "Role",
                value: 1);

            migrationBuilder.InsertData(
                table: "registered_user",
                columns: new[] { "id", "birth_date", "email", "is_blocked", "name", "password", "phone", "Role", "surname", "UserRoleId" },
                values: new object[,]
                {
                    { 3, "1990-10-10", "test@test.com", false, "Waiter2", "password", "123", 2, "Shakespeare", null },
                    { 2, "1990-10-10", "test@test.com", false, "Waiter", "password", "123", 2, "Shakespeare", null }
                });
        }
    }
}
