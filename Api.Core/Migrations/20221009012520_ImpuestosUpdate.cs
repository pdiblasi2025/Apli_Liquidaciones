using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class ImpuestosUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientesImpuestos_ErpMilongaTaxTypeDbSet_ErpMilongaTaxTypeId",
                table: "ClientesImpuestos");

            migrationBuilder.DropIndex(
                name: "IX_ClientesImpuestos_ErpMilongaTaxTypeId",
                table: "ClientesImpuestos");

            migrationBuilder.DropColumn(
                name: "ErpMilongaTaxTypeId",
                table: "ClientesImpuestos");

            migrationBuilder.AddColumn<int>(
                name: "ErpMilongaTaxCodeId",
                table: "ClientesImpuestos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ErpMilongaTaxCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaxCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ErpMilongaTaxTypeId",
                table: "ClientesImpuestos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesImpuestos_ErpMilongaTaxTypeId",
                table: "ClientesImpuestos",
                column: "ErpMilongaTaxTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientesImpuestos_ErpMilongaTaxTypeDbSet_ErpMilongaTaxTypeId",
                table: "ClientesImpuestos",
                column: "ErpMilongaTaxTypeId",
                principalTable: "ErpMilongaTaxTypeDbSet",
                principalColumn: "Id");
        }
    }
}
