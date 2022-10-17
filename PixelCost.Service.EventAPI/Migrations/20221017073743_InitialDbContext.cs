using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.EventAPI.Migrations
{
    public partial class InitialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationId = table.Column<long>(type: "bigint", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    IsAchived = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
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
                    SubDurationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Events_SubDurationId",
                        column: x => x.SubDurationId,
                        principalTable: "Events",
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
                    SubDurationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revenues_Events_SubDurationId",
                        column: x => x.SubDurationId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubDurationId",
                table: "Categories",
                column: "SubDurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_SubDurationId",
                table: "Revenues",
                column: "SubDurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
