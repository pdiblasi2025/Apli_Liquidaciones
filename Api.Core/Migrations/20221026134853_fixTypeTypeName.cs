using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class fixTypeTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ErpMilongaTaxTypeDbSet");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "ErpMilongaTaxTypeDbSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "ErpMilongaTaxTypeDbSet");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ErpMilongaTaxTypeDbSet",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
