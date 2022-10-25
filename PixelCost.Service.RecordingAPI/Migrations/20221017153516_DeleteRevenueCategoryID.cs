using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.RecordingAPI.Migrations
{
    public partial class DeleteRevenueCategoryID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Revenues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Revenues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
