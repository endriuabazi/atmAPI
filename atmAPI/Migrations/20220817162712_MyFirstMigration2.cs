using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atmAPI.Migrations
{
    public partial class MyFirstMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "transactions",
                newName: "accountid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "accountid",
                table: "transactions",
                newName: "account_id");
        }
    }
}
