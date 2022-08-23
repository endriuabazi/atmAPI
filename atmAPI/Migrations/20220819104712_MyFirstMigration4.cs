using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atmAPI.Migrations
{
    public partial class MyFirstMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "accountid",
                table: "transactions",
                newName: "account_id");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_account_id",
                table: "transactions",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_accounts_account_id",
                table: "transactions",
                column: "account_id",
                principalTable: "accounts",
                principalColumn: "account_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_accounts_account_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_account_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "username",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "transactions",
                newName: "accountid");
        }
    }
}
