using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class liquidacionPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoEnvio",
                table: "DetalleLiquidacionPos");

            migrationBuilder.DropColumn(
                name: "OtrosGastos",
                table: "DetalleLiquidacionPos");

            migrationBuilder.AddColumn<string>(
                name: "Depto",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoImpuestoId",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LiquidacionPago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdLiquidacion = table.Column<int>(nullable: false),
                    ReciboId = table.Column<int>(nullable: false),
                    NumeroRecibo = table.Column<string>(nullable: true),
                    LinkPdf = table.Column<string>(nullable: true),
                    LiquidacionId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidacionPago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiquidacionPago_Liquidaciones_IdLiquidacion",
                        column: x => x.IdLiquidacion,
                        principalTable: "Liquidaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LiquidacionPago_Liquidaciones_LiquidacionId1",
                        column: x => x.LiquidacionId1,
                        principalTable: "Liquidaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoImpuestoId",
                table: "Clientes",
                column: "TipoImpuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidacionPago_IdLiquidacion",
                table: "LiquidacionPago",
                column: "IdLiquidacion");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidacionPago_LiquidacionId1",
                table: "LiquidacionPago",
                column: "LiquidacionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_ErpMilongaTaxTypeDbSet_TipoImpuestoId",
                table: "Clientes",
                column: "TipoImpuestoId",
                principalTable: "ErpMilongaTaxTypeDbSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_ErpMilongaTaxTypeDbSet_TipoImpuestoId",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "LiquidacionPago");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_TipoImpuestoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Depto",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TipoImpuestoId",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "CodigoEnvio",
                table: "DetalleLiquidacionPos",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OtrosGastos",
                table: "DetalleLiquidacionPos",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
