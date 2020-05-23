using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class CustomerTableDependencyChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fkc_customer_tables",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_fk_customer_tables",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "fk_customer_tables",
                table: "orders");

            migrationBuilder.AddColumn<long>(
                name: "FkCustomerTables",
                table: "bills",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FkCustomerTablesNavigationId",
                table: "bills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bills_FkCustomerTablesNavigationId",
                table: "bills",
                column: "FkCustomerTablesNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_bills_customer_tables_FkCustomerTablesNavigationId",
                table: "bills",
                column: "FkCustomerTablesNavigationId",
                principalTable: "customer_tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bills_customer_tables_FkCustomerTablesNavigationId",
                table: "bills");

            migrationBuilder.DropIndex(
                name: "IX_bills_FkCustomerTablesNavigationId",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "FkCustomerTables",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "FkCustomerTablesNavigationId",
                table: "bills");

            migrationBuilder.AddColumn<long>(
                name: "fk_customer_tables",
                table: "orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_orders_fk_customer_tables",
                table: "orders",
                column: "fk_customer_tables");

            migrationBuilder.AddForeignKey(
                name: "fkc_customer_tables",
                table: "orders",
                column: "fk_customer_tables",
                principalTable: "customer_tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
