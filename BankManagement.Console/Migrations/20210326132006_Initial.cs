using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Console.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankUId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountHolders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: false),
                    BankUId = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    AvailableBalance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountHolders_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    ExcahngeRate = table.Column<double>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    BankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: false),
                    BankUId = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCharges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SameBankRTGS = table.Column<double>(nullable: false),
                    SameBankIMPS = table.Column<double>(nullable: false),
                    OtherBankRTGS = table.Column<double>(nullable: false),
                    OtherBankIMPS = table.Column<double>(nullable: false),
                    BankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCharges_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionUId = table.Column<string>(nullable: true),
                    SrcAccountNumber = table.Column<string>(nullable: true),
                    AccountHolderId = table.Column<int>(nullable: false),
                    DestAccountNumber = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    SrcBankID = table.Column<string>(nullable: true),
                    DestBankID = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    CreatedOn = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    IsReverted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AccountHolders_AccountHolderId",
                        column: x => x.AccountHolderId,
                        principalTable: "AccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountHolders_BankId",
                table: "AccountHolders",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_BankId",
                table: "Currencies",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BankId",
                table: "Employees",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_BankId",
                table: "ServiceCharges",
                column: "BankId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountHolderId",
                table: "Transactions",
                column: "AccountHolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ServiceCharges");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AccountHolders");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
