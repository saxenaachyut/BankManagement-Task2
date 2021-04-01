using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Console.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Banks_BankId1",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_AccountHolderId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountHolderId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_BankId1",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "AccountHolderId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BankId1",
                table: "Currencies");

            migrationBuilder.AlterColumn<string>(
                name: "SrcBankID",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DestBankID",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AccountHolderId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BankId",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountHolderId",
                table: "Transactions",
                column: "AccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_BankId",
                table: "Currencies",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Banks_BankId",
                table: "Currencies",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_AccountHolderId",
                table: "Transactions",
                column: "AccountHolderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Banks_BankId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_AccountHolderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountHolderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_BankId",
                table: "Currencies");

            migrationBuilder.AlterColumn<int>(
                name: "SrcBankID",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DestBankID",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountHolderId",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountHolderId1",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                table: "Currencies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankId1",
                table: "Currencies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountHolderId1",
                table: "Transactions",
                column: "AccountHolderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_BankId1",
                table: "Currencies",
                column: "BankId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Banks_BankId1",
                table: "Currencies",
                column: "BankId1",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_AccountHolderId1",
                table: "Transactions",
                column: "AccountHolderId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
