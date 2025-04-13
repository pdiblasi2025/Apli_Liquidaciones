using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class ClienteImpuesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Jurisdiccion_JurisdiccionId",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "Jurisdiccion");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_JurisdiccionId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "JurisdiccionId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "MetodoEnvio",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NombreUsuario",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "Clientes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetodosEnvio",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientesImpuestos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    ErpMilongaTaxTypeId = table.Column<int>(nullable: false),
                    PorcentajeExencion = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    ExencionDesde = table.Column<DateTime>(nullable: true),
                    ExencionHasta = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesImpuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesImpuestos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientesImpuestos_ErpMilongaTaxTypeDbSet_ErpMilongaTaxTypeId",
                        column: x => x.ErpMilongaTaxTypeId,
                        principalTable: "ErpMilongaTaxTypeDbSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesImpuestos_ClienteId",
                table: "ClientesImpuestos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesImpuestos_ErpMilongaTaxTypeId",
                table: "ClientesImpuestos",
                column: "ErpMilongaTaxTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesImpuestos");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "MetodosEnvio",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "CodigoPostal",
                table: "Clientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JurisdiccionId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MetodoEnvio",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreUsuario",
                table: "Clientes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Jurisdiccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurisdiccion", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Jurisdiccion",
                columns: new[] { "Id", "Codigo", "Descripcion" },
                values: new object[,]
                {
                    { 1, "901", "Capital Federal" },
                    { 23, "923", "Tierra del Fuego" },
                    { 22, "922", "Santiago del Estero" },
                    { 21, "921", "Santa Fe" },
                    { 20, "920", "Santa Cruz" },
                    { 19, "919", "San Luis" },
                    { 18, "918", "San Juan" },
                    { 17, "917", "Salta" },
                    { 16, "916", "Rio Negro" },
                    { 15, "915", "Neuquen" },
                    { 14, "914", "Misiones" },
                    { 24, "924", "Tucuman" },
                    { 13, "913", "Mendoza" },
                    { 11, "911", "La Pampa" },
                    { 10, "910", "Jujuy" },
                    { 9, "909", "Formosa" },
                    { 8, "908", "Entre Rios" },
                    { 7, "907", "Chubut" },
                    { 6, "906", "Chaco" },
                    { 5, "905", "Corrientes" },
                    { 4, "904", "Cordoba" },
                    { 3, "903", "Catamarca" },
                    { 2, "902", "Buenos Aires" },
                    { 12, "912", "La Rioja" },
                    { 25, "Z", "No aplicable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_JurisdiccionId",
                table: "Clientes",
                column: "JurisdiccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Jurisdiccion_JurisdiccionId",
                table: "Clientes",
                column: "JurisdiccionId",
                principalTable: "Jurisdiccion",
                principalColumn: "Id");
        }
    }
}
