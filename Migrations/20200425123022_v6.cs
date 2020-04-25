using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace smart_table.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_registered_user_UserRoles_UserRoleId",
            //    table: "registered_user");

            //migrationBuilder.DropTable(
            //    name: "UserRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_registered_user_UserRoleId",
            //    table: "registered_user");

            //migrationBuilder.DropColumn(
            //    name: "UserRoleId",
            //    table: "registered_user");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "registered_user",
                newName: "role");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "registered_user",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 1,
                column: "role",
                value: "Administrator");

            migrationBuilder.InsertData(
                table: "registered_user",
                columns: new[] { "id", "birth_date", "email", "is_blocked", "name", "password", "phone", "role", "surname" },
                values: new object[] { 2, "1990-10-10", "test@test.com", false, "Waiter", "password", "123", "Waiter", "Shakespeare" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "role",
                table: "registered_user",
                newName: "Role");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "registered_user",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "registered_user",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "registered_user",
                keyColumn: "id",
                keyValue: 1,
                column: "Role",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_registered_user_UserRoleId",
                table: "registered_user",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_registered_user_UserRoles_UserRoleId",
                table: "registered_user",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
