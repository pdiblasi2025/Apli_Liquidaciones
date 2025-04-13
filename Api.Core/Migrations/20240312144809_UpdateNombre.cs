using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class UpdateNombre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE Clientes SET Nombre = CONCAT(Nombre,' ',Apellido);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
