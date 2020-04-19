using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICategoriasProdutos.Migrations
{
    public partial class removendo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teste",
                table: "Categoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teste",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
