using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace smart_table.Migrations
{
    public partial class Initial_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer_tables",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    seats_count = table.Column<int>(nullable: false),
                    qr_code = table.Column<string>(maxLength: 255, nullable: true),
                    is_taken = table.Column<bool>(nullable: false),
                    join_code = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_tables", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    discount_code = table.Column<string>(maxLength: 255, nullable: false),
                    stand_until = table.Column<DateTime>(type: "date", nullable: true),
                    discount_proc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dish_categories",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dish_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_type",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(fixedLength: true, maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(maxLength: 255, nullable: false),
                    mon = table.Column<bool>(nullable: false),
                    tue = table.Column<bool>(nullable: false),
                    wed = table.Column<bool>(nullable: false),
                    thu = table.Column<bool>(nullable: false),
                    fri = table.Column<bool>(nullable: false),
                    sat = table.Column<bool>(nullable: false),
                    sun = table.Column<bool>(nullable: false),
                    time_from = table.Column<DateTime>(type: "date", nullable: true),
                    time_until = table.Column<DateTime>(type: "date", nullable: true),
                    date_from = table.Column<DateTime>(type: "date", nullable: true),
                    date_until = table.Column<DateTime>(type: "date", nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(fixedLength: true, maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_time = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "CURRENT_DATE"),
                    tips = table.Column<double>(nullable: true),
                    amount = table.Column<double>(nullable: false),
                    is_paid = table.Column<bool>(nullable: false),
                    evaluation = table.Column<string>(maxLength: 255, nullable: false),
                    fk_discounts = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bills", x => x.id);
                    table.ForeignKey(
                        name: "fkc_discounts",
                        column: x => x.fk_discounts,
                        principalTable: "discounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dishes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(maxLength: 255, nullable: false),
                    description = table.Column<string>(maxLength: 255, nullable: true),
                    price = table.Column<double>(nullable: false),
                    calories = table.Column<int>(nullable: true),
                    discount = table.Column<double>(nullable: true),
                    fk_dish_categories = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dishes", x => x.id);
                    table.ForeignKey(
                        name: "fkc_dish_categories",
                        column: x => x.fk_dish_categories,
                        principalTable: "dish_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "registered_users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    surname = table.Column<string>(maxLength: 255, nullable: false),
                    password = table.Column<string>(maxLength: 255, nullable: false),
                    phone = table.Column<string>(maxLength: 255, nullable: true),
                    email = table.Column<string>(maxLength: 255, nullable: true),
                    birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    is_blocked = table.Column<bool>(nullable: false),
                    role = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registered_users", x => x.id);
                    table.ForeignKey(
                        name: "registered_users_role_fkey",
                        column: x => x.role,
                        principalTable: "user_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dish_ingredients",
                columns: table => new
                {
                    fk_dishes = table.Column<long>(nullable: false),
                    fk_ingredients = table.Column<long>(nullable: false),
                    quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("dish_ingredients_pkey", x => new { x.fk_dishes, x.fk_ingredients });
                    table.ForeignKey(
                        name: "fkc_dishes",
                        column: x => x.fk_dishes,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkc_ingredients",
                        column: x => x.fk_ingredients,
                        principalTable: "ingredients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "menu_dishes",
                columns: table => new
                {
                    fk_dishes = table.Column<long>(nullable: false),
                    fk_menus = table.Column<long>(nullable: false),
                    date_from = table.Column<DateTime>(type: "date", nullable: true),
                    date_until = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("menu_dishes_pkey", x => new { x.fk_dishes, x.fk_menus });
                    table.ForeignKey(
                        name: "fkc_dishes",
                        column: x => x.fk_dishes,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkc_menus",
                        column: x => x.fk_menus,
                        principalTable: "menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_time = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "CURRENT_DATE"),
                    temperature = table.Column<double>(nullable: true),
                    submitted = table.Column<bool>(nullable: false),
                    served = table.Column<bool>(nullable: false),
                    fk_bills = table.Column<long>(nullable: true),
                    fk_registered_users = table.Column<long>(nullable: true),
                    fk_customer_tables = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "fkc_bills",
                        column: x => x.fk_bills,
                        principalTable: "bills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkc_customer_tables",
                        column: x => x.fk_customer_tables,
                        principalTable: "customer_tables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkc_registered_users",
                        column: x => x.fk_registered_users,
                        principalTable: "registered_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<long>(nullable: false),
                    fk_orders = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                    table.ForeignKey(
                        name: "fkc_orders",
                        column: x => x.fk_orders,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "events_type_fkey",
                        column: x => x.type,
                        principalTable: "event_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_dishes",
                columns: table => new
                {
                    fk_orders = table.Column<long>(nullable: false),
                    fk_dishes = table.Column<long>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    comment = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("order_dishes_pkey", x => new { x.fk_dishes, x.fk_orders });
                    table.ForeignKey(
                        name: "fkc_dishes",
                        column: x => x.fk_dishes,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkc_orders",
                        column: x => x.fk_orders,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bills_fk_discounts",
                table: "bills",
                column: "fk_discounts");

            migrationBuilder.CreateIndex(
                name: "IX_dish_ingredients_fk_ingredients",
                table: "dish_ingredients",
                column: "fk_ingredients");

            migrationBuilder.CreateIndex(
                name: "IX_dishes_fk_dish_categories",
                table: "dishes",
                column: "fk_dish_categories");

            migrationBuilder.CreateIndex(
                name: "IX_events_fk_orders",
                table: "events",
                column: "fk_orders");

            migrationBuilder.CreateIndex(
                name: "IX_events_type",
                table: "events",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_menu_dishes_fk_menus",
                table: "menu_dishes",
                column: "fk_menus");

            migrationBuilder.CreateIndex(
                name: "IX_order_dishes_fk_orders",
                table: "order_dishes",
                column: "fk_orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_fk_bills",
                table: "orders",
                column: "fk_bills");

            migrationBuilder.CreateIndex(
                name: "IX_orders_fk_customer_tables",
                table: "orders",
                column: "fk_customer_tables");

            migrationBuilder.CreateIndex(
                name: "IX_orders_fk_registered_users",
                table: "orders",
                column: "fk_registered_users");

            migrationBuilder.CreateIndex(
                name: "IX_registered_users_role",
                table: "registered_users",
                column: "role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dish_ingredients");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "menu_dishes");

            migrationBuilder.DropTable(
                name: "order_dishes");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "event_type");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "dishes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "dish_categories");

            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.DropTable(
                name: "customer_tables");

            migrationBuilder.DropTable(
                name: "registered_users");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "user_role");
        }
    }
}
