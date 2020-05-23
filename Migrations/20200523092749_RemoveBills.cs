using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_table.Migrations
{
    public partial class RemoveBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "bills",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "bills",
                keyColumn: "id",
                keyValue: 2L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "bills",
                columns: new[] { "id", "amount", "date_time", "evaluation", "fk_discounts", "is_paid", "tips" },
                values: new object[,]
                {
                    { 1L, 50.450000000000003, new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Labai skanus maistas", 1L, true, 10.0 },
                    { 2L, 5.3899999999999997, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malonus aptarnavimas", 2L, false, 0.0 }
                });
        }
    }
}
