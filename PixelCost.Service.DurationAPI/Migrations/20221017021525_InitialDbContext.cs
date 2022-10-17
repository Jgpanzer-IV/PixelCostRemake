using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.DurationAPI.Migrations
{
    public partial class InitialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Durations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: true),
                    RemainingDays = table.Column<int>(type: "int", nullable: true),
                    Progress = table.Column<float>(type: "real", nullable: true),
                    InitialCost = table.Column<decimal>(type: "money", nullable: false),
                    TotalCost = table.Column<decimal>(type: "money", nullable: true),
                    SumCategoryCost = table.Column<decimal>(type: "money", nullable: true),
                    SumSubDurationCost = table.Column<decimal>(type: "money", nullable: true),
                    UsableMoney = table.Column<decimal>(type: "money", nullable: true),
                    Revenue = table.Column<decimal>(type: "money", nullable: true),
                    RevenueCount = table.Column<int>(type: "int", nullable: true),
                    Expense = table.Column<decimal>(type: "money", nullable: true),
                    ExpenseCount = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<decimal>(type: "money", nullable: true),
                    SumCategoryBalance = table.Column<decimal>(type: "money", nullable: true),
                    SumSubDurationBalance = table.Column<decimal>(type: "money", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    Expense = table.Column<decimal>(type: "money", nullable: true),
                    ExpenseCount = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<decimal>(type: "money", nullable: true),
                    IsAchived = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    DurationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryExpenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderingPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryExpenses_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EarningAmount = table.Column<decimal>(type: "money", nullable: false),
                    EarningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revenues_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubDurations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: true),
                    RemainingDays = table.Column<int>(type: "int", nullable: true),
                    Progress = table.Column<float>(type: "real", nullable: true),
                    InitialCost = table.Column<decimal>(type: "money", nullable: false),
                    TotalCost = table.Column<decimal>(type: "money", nullable: true),
                    SumCategoryCost = table.Column<decimal>(type: "money", nullable: true),
                    UsableMoney = table.Column<decimal>(type: "money", nullable: true),
                    Revenue = table.Column<decimal>(type: "money", nullable: true),
                    RevenueCount = table.Column<int>(type: "int", nullable: true),
                    Expense = table.Column<decimal>(type: "money", nullable: true),
                    ExpenseCount = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<decimal>(type: "money", nullable: true),
                    SumCategoryBalance = table.Column<decimal>(type: "money", nullable: true),
                    IsAchived = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    DurationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubDurations_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DurationId",
                table: "Categories",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryExpenses_DurationId",
                table: "PrimaryExpenses",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_DurationId",
                table: "Revenues",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubDurations_DurationId",
                table: "SubDurations",
                column: "DurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PrimaryExpenses");

            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.DropTable(
                name: "SubDurations");

            migrationBuilder.DropTable(
                name: "Durations");
        }
    }
}
