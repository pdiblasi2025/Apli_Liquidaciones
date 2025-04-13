using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class addErpLiquidacionIdLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LiquidacionId",
                table: "ErpInvoiceSyncLogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiquidacionId",
                table: "ErpInvoiceSyncLogs");
        }
    }
}
