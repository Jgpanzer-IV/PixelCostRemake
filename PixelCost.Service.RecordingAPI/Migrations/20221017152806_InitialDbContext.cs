using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.RecordingAPI.Migrations
{
    public partial class InitialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderingName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OrderingPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationId = table.Column<long>(type: "bigint", nullable: false),
                    SubDurationId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
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
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationId = table.Column<long>(type: "bigint", nullable: false),
                    SubDurationId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryExpenses", x => x.Id);
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
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationId = table.Column<long>(type: "bigint", nullable: false),
                    SubDurationId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "PrimaryExpenses");

            migrationBuilder.DropTable(
                name: "Revenues");
        }
    }
}
