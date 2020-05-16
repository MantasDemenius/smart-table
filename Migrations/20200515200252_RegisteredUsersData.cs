using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class RegisteredUsersData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "registered_users",
                columns: new[] { "id", "birth_date", "email", "is_blocked", "name", "password", "phone", "role", "surname" },
                values: new object[,]
                {
                    { 1L, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, "User1", "Password123", "860000000", 1L, "User1" },
                    { 2L, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", true, "User2", "Password456", "", 2L, "User2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "registered_users",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
