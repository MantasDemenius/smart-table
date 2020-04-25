using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace smart_table.Migrations
{
    public partial class RegisteredUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.CreateTable(
                name: "registered_user",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    surname = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    birth_date = table.Column<string>(nullable: true),
                    role = table.Column<string>(nullable: true),
                    is_blocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registered_user", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "registered_user",
                columns: new[] { "id", "birth_date", "email", "is_blocked", "name", "password", "phone", "role", "surname" },
                values: new object[] { 1, "1990-10-10", "test@test.com", false, "William", "password", "123", "Administrator", "Shakespeare" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registered_user");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    blocked = table.Column<bool>(type: "boolean", nullable: false),
                    date_of_birth = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "blocked", "date_of_birth", "email", "first_name", "last_name", "password", "phone_number", "type" },
                values: new object[] { 1, false, "1990-10-10", "test@test.com", "William", "Shakespeare", "password", "123", 0 });
        }
    }
}
