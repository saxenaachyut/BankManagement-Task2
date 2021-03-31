using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Console.Migrations
{
    public partial class changetransactiondef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SrcBankID",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DestBankID",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SrcBankID",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "DestBankID",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
