using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class clienteTipoDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoDocumentoId",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoDocumentoId",
                table: "Clientes",
                column: "TipoDocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_ErpMilongaIdentificationTypeDbSet_TipoDocumentoId",
                table: "Clientes",
                column: "TipoDocumentoId",
                principalTable: "ErpMilongaIdentificationTypeDbSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_ErpMilongaIdentificationTypeDbSet_TipoDocumentoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_TipoDocumentoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TipoDocumentoId",
                table: "Clientes");
        }
    }
}
