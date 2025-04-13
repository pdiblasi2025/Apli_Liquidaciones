using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallbackasociadoMP",
                table: "DetalleLiquidacionPre");

            migrationBuilder.DropColumn(
                name: "CostoEnvioReal",
                table: "DetalleLiquidacionPos");

            migrationBuilder.DropColumn(
                name: "CostoEnvioTarifado",
                table: "DetalleLiquidacionPos");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "DetalleLiquidacionPos");

            migrationBuilder.AlterColumn<string>(
                name: "IdPreferenciaMP",
                table: "DetalleLiquidacionPre",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdOrdenPago",
                table: "DetalleLiquidacionPre",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdMercadoPago",
                table: "DetalleLiquidacionPre",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleLiquidacionPre_OmsId",
                table: "DetalleLiquidacionPre",
                column: "OmsId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleLiquidacionPos_OmsId",
                table: "DetalleLiquidacionPos",
                column: "OmsId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_OmsId",
                table: "Clientes",
                column: "OmsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DetalleLiquidacionPre_OmsId",
                table: "DetalleLiquidacionPre");

            migrationBuilder.DropIndex(
                name: "IX_DetalleLiquidacionPos_OmsId",
                table: "DetalleLiquidacionPos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_OmsId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "IdPreferenciaMP",
                table: "DetalleLiquidacionPre",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdOrdenPago",
                table: "DetalleLiquidacionPre",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdMercadoPago",
                table: "DetalleLiquidacionPre",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CallbackasociadoMP",
                table: "DetalleLiquidacionPre",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoEnvioReal",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoEnvioTarifado",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "DetalleLiquidacionPos",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
