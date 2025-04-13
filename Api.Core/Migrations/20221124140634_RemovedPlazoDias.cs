using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class RemovedPlazoDias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlazoDias",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "TerminoPago",
                table: "CondicionesPago",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerminoPago",
                table: "CondicionesPago");

            migrationBuilder.AddColumn<int>(
                name: "PlazoDias",
                table: "Clientes",
                type: "int",
                nullable: true);
        }
    }
}
