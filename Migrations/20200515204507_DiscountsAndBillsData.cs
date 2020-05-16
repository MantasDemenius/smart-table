using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class DiscountsAndBillsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "discounts",
                columns: new[] { "id", "discount_code", "discount_proc", "stand_until" },
                values: new object[,]
                {
                    { 1L, "ST101", 15, new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, "ST102", 25, new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "discounts",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "discounts",
                keyColumn: "id",
                keyValue: 1L);
        }
    }
}
