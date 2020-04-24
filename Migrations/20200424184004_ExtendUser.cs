using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class ExtendUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "user");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "user");

            migrationBuilder.AddColumn<bool>(
                name: "blocked",
                table: "user",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "date_of_birth",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "user",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "blocked", "date_of_birth", "email", "first_name", "last_name", "password", "phone_number", "type" },
                values: new object[] { 1, false, "1990-10-10", "test@test.com", "William", "Shakespeare", "password", "123", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "blocked",
                table: "user");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "user");

            migrationBuilder.DropColumn(
                name: "password",
                table: "user");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "user");

            migrationBuilder.DropColumn(
                name: "type",
                table: "user");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
