using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICategoriasProdutos.Migrations
{
    public partial class PopulaDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categoria(ImageUrl,Nome) values('http://localhost:50001/images','Eletrônicos')");


            migrationBuilder.Sql("insert into Produto(Nome,Descricao,Preco,ImageUrl,Estoque,DataCadastro,CategoriaId) values('IPHONE','SMARTPHONE',200.00,'http://localhost:50001/images',10,GETDATE(),(SELECT CategoriaId from Categoria where Nome='Eletrônicos'))");

            migrationBuilder.Sql("insert into Categoria(ImageUrl,Nome) values('http://localhost:50001/images','Papelaria')");


            migrationBuilder.Sql("insert into Produto(Nome,Descricao,Preco,ImageUrl,Estoque,DataCadastro,CategoriaId) values('PASTA','PASTA COM DIVISÓRIAS',30.50,'http://localhost:50001/images',10,GETDATE(),(SELECT CategoriaId from Categoria where Nome='Papelaria'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
