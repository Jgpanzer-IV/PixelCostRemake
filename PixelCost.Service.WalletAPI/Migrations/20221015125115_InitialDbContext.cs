using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.WalletAPI.Migrations
{
    public partial class InitialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Balance = table.Column<decimal>(type: "money", nullable: true),
                    TotalExpense = table.Column<decimal>(type: "money", nullable: true),
                    TotalNumberExpense = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalRevenue = table.Column<decimal>(type: "money", nullable: true),
                    TotalNumberRevenue = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PaymentRevenue = table.Column<decimal>(type: "money", nullable: true),
                    PaymentRevenueCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PaymentBalance = table.Column<decimal>(type: "money", nullable: true),
                    PaymentExpense = table.Column<decimal>(type: "money", nullable: true),
                    PaymentExpenseCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AverageUsedPerPayment = table.Column<decimal>(type: "money", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    WalletID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Wallets_WalletID",
                        column: x => x.WalletID,
                        principalTable: "Wallets",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_WalletID",
                table: "PaymentMethods",
                column: "WalletID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
