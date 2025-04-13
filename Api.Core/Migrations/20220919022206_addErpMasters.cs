using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class addErpMasters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CondicionesPago",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CondicionesPago",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CondicionesPago",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "ErpId",
                table: "CondicionesPago",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ErpMilongaIdentificationTypeDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaxTypeID = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpMilongaIdentificationTypeDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErpMilongaProductTypeDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductTypeID = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpMilongaProductTypeDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErpMilongaTaxTypeDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaxTypeID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpMilongaTaxTypeDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErpMilongaUnitOfMeasureDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnitOfMeasureID = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpMilongaUnitOfMeasureDbSet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErpMilongaIdentificationTypeDbSet");

            migrationBuilder.DropTable(
                name: "ErpMilongaProductTypeDbSet");

            migrationBuilder.DropTable(
                name: "ErpMilongaTaxTypeDbSet");

            migrationBuilder.DropTable(
                name: "ErpMilongaUnitOfMeasureDbSet");

            migrationBuilder.DropColumn(
                name: "ErpId",
                table: "CondicionesPago");

            migrationBuilder.InsertData(
                table: "CondicionesPago",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 1, "A 30 días FF" });

            migrationBuilder.InsertData(
                table: "CondicionesPago",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 2, "A 45 días FF" });

            migrationBuilder.InsertData(
                table: "CondicionesPago",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 3, "A 60 días FF" });
        }
    }
}
