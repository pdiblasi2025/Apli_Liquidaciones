using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class ImpuestoUpdateV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientesImpuestos_ErpMilongaTaxCodes_ErpMilongaTaxCodeId",
                table: "ClientesImpuestos");

            migrationBuilder.DropTable(
                name: "ErpMilongaTaxCodes");

            migrationBuilder.DropIndex(
                name: "IX_ClientesImpuestos_ErpMilongaTaxCodeId",
                table: "ClientesImpuestos");

            migrationBuilder.DropColumn(
                name: "ErpMilongaTaxCodeId",
                table: "ClientesImpuestos");

            migrationBuilder.AddColumn<int>(
                name: "ImpuestoId",
                table: "ClientesImpuestos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Impuestos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impuestos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesImpuestos_ImpuestoId",
                table: "ClientesImpuestos",
                column: "ImpuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientesImpuestos_Impuestos_ImpuestoId",
                table: "ClientesImpuestos",
                column: "ImpuestoId",
                principalTable: "Impuestos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientesImpuestos_Impuestos_ImpuestoId",
                table: "ClientesImpuestos");

            migrationBuilder.DropTable(
                name: "Impuestos");

            migrationBuilder.DropIndex(
                name: "IX_ClientesImpuestos_ImpuestoId",
                table: "ClientesImpuestos");

            migrationBuilder.DropColumn(
                name: "ImpuestoId",
                table: "ClientesImpuestos");

            migrationBuilder.AddColumn<int>(
                name: "ErpMilongaTaxCodeId",
                table: "ClientesImpuestos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ErpMilongaTaxCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TaxCode = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpMilongaTaxCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesImpuestos_ErpMilongaTaxCodeId",
                table: "ClientesImpuestos",
                column: "ErpMilongaTaxCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientesImpuestos_ErpMilongaTaxCodes_ErpMilongaTaxCodeId",
                table: "ClientesImpuestos",
                column: "ErpMilongaTaxCodeId",
                principalTable: "ErpMilongaTaxCodes",
                principalColumn: "Id");
        }
    }
}
