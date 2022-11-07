using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelCost.Service.RecordingAPI.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PrimaryExpenses");

            migrationBuilder.RenameColumn(
                name: "SubDurationId",
                table: "PrimaryExpenses",
                newName: "PaymentMethodId");

            migrationBuilder.AlterColumn<long>(
                name: "SubDurationId",
                table: "Revenues",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PaymentMethodId",
                table: "Revenues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "SubDurationId",
                table: "Expenses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PaymentMethodId",
                table: "Expenses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Revenues");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "PrimaryExpenses",
                newName: "SubDurationId");

            migrationBuilder.AlterColumn<long>(
                name: "SubDurationId",
                table: "Revenues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "PrimaryExpenses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "SubDurationId",
                table: "Expenses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
