using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "registered_user");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "registered_user",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "registered_user");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "registered_user",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
