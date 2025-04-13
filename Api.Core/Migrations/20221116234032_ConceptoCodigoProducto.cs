using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class ConceptoCodigoProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoProductoId",
                table: "Conceptos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conceptos_CodigoProductoId",
                table: "Conceptos",
                column: "CodigoProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conceptos_ErpMilongaProductCodes_CodigoProductoId",
                table: "Conceptos",
                column: "CodigoProductoId",
                principalTable: "ErpMilongaProductCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conceptos_ErpMilongaProductCodes_CodigoProductoId",
                table: "Conceptos");

            migrationBuilder.DropIndex(
                name: "IX_Conceptos_CodigoProductoId",
                table: "Conceptos");

            migrationBuilder.DropColumn(
                name: "CodigoProductoId",
                table: "Conceptos");
        }
    }
}
