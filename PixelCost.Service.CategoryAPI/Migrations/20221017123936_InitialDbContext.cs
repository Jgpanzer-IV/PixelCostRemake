using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.CategoryAPI.Migrations
{
    public partial class InitialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    DurationId = table.Column<long>(type: "bigint", nullable: false),
                    SubDurationId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderingPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
