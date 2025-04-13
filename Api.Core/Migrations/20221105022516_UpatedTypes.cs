using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class UpatedTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "DetalleLiquidacionPre",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Volumen",
                table: "DetalleLiquidacionPos",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "Peso",
                table: "DetalleLiquidacionPos",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "Largo",
                table: "DetalleLiquidacionPos",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "Cantidad",
                table: "DetalleLiquidacionPos",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "Ancho",
                table: "DetalleLiquidacionPos",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "Alto",
                table: "DetalleLiquidacionPos",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "DetalleLiquidacionPos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OmsSyncLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Log = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    JobType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OmsSyncLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OmsSyncLogs");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "DetalleLiquidacionPre");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "DetalleLiquidacionPos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Volumen",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Largo",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ancho",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Alto",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
