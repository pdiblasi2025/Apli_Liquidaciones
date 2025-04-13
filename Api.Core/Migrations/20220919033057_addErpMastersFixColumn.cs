using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class addErpMastersFixColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxTypeID",
                table: "ErpMilongaIdentificationTypeDbSet");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationTypeID",
                table: "ErpMilongaIdentificationTypeDbSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationTypeID",
                table: "ErpMilongaIdentificationTypeDbSet");

            migrationBuilder.AddColumn<string>(
                name: "TaxTypeID",
                table: "ErpMilongaIdentificationTypeDbSet",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
