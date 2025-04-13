using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class RemovedEstadoEnvio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoEnvio",
                table: "DetalleLiquidacionPos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoEnvio",
                table: "DetalleLiquidacionPos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
