using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.PaymentAPI.Migrations
{
    public partial class InitialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PaymentRevenue = table.Column<decimal>(type: "money", nullable: true),
                    PaymentRevenueCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PaymentBalance = table.Column<decimal>(type: "money", nullable: true),
                    PaymentExpense = table.Column<decimal>(type: "money", nullable: true),
                    PaymentExpenseCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AverageUsedPerPayment = table.Column<decimal>(type: "money", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderingName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OrderingPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_PaymentMethods_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryExpenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderingName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OrderingPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryExpenses_PaymentMethods_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EarningAmount = table.Column<decimal>(type: "money", nullable: false),
                    EarningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revenues_PaymentMethods_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentId",
                table: "Expenses",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryExpenses_PaymentId",
                table: "PrimaryExpenses",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_PaymentId",
                table: "Revenues",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "PrimaryExpenses");

            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.DropTable(
                name: "PaymentMethods");
        }
    }
}
