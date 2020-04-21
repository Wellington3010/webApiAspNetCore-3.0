using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICategoriasProdutos.Data;
using WebAPICategoriasProdutos.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;

namespace WebAPICategoriasProdutos.Controllers
{
    [Route("Produtos")]
    [ApiController]
    [Produces("application/json")]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        [Route("ListarProdutos")]
        [EnableCors("DefaultCors")]
        public async Task<List<Produto>> GetProdutos([FromServices] DataContext ctx)
        {
            
             var retorno =  await ctx.Produtos.ToListAsync();
             return retorno;

        }

        [HttpGet]
        [Route("produto/{id}")]
        public async Task<ActionResult<Produto>> FyndById([FromServices] DataContext ctx,[FromRoute] int id)
        {

            var produto = await ctx.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.ProdutoId == id);

            return produto;

        }


        [HttpPost]
        [Route("cadastrar")]
        public async Task<HttpResponse> CadastrarProduto([FromServices] DataContext ctx,[FromBody] Produto model)
        {
            if (ModelState.IsValid)
            {
                model.DataCadastro = DateTime.Now;
                ctx.Produtos.Add(model);

                await ctx.SaveChangesAsync();

                Response.ContentType = "application/json";
                await Response.WriteAsync("Cadastro realizado com sucesso");
                Response.StatusCode = 200;
                return Response;
            }
            else
            {
                Response.ContentType = "application/json";
                await Response.WriteAsync("Erro ao cadastrar. Verifique se os dados da requisição estão corretos.");
                Response.StatusCode = 400;
                return Response;
            }

        }


        [HttpPut]
        [Route("AlterarProduto/{id}")]
        public async Task<ActionResult> AlterarProduto([FromServices]DataContext ctx,int id,[FromBody] Produto produto)
        {
            if(ModelState.IsValid)
            {
                if(id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                if(!await ctx.Produtos.AnyAsync(x => x.ProdutoId == id))
                {
                    return NotFound();
                }

                ctx.Entry(produto).State = EntityState.Modified;
                ctx.SaveChanges();

                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }




        [HttpDelete]
        [Route("DeletarProduto/{id}")]
        public async Task<ActionResult> DeletarProduto([FromServices] DataContext ctx,[FromRoute] int id)
        {
               var produto = await ctx.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

               if (produto != null)
                {
                    ctx.Produtos.Remove(produto);
                    await ctx.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
        }

    }
}