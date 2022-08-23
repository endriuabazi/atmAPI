using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atmAPI.Migrations
{
    public partial class MyFirstMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transaction_time",
                table: "transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "transaction_time",
                table: "transactions",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
