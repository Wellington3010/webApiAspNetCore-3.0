using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPICategoriasProdutos.Data;
using WebAPICategoriasProdutos.Models;

namespace WebAPICategoriasProdutos.Controllers
{
    /// <summary>
    /// Categorias
    /// </summary>
    [Route("Categorias")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoriasController : ControllerBase
    {


        private readonly DataContext ctx;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="context"></param>
        public CategoriasController(DataContext context)
        {
            ctx = context;
        }

        /// <summary>
        /// Lista de categorias
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo de request
        /// 
        /// GET Categorias/ListarCategorias
        /// 
        /// </remarks>
        /// <returns>ActionResult</returns>
        [HttpGet]
        [Route("ListarCategorias")]
        public async Task<List<Categoria>> GetCategorias()
        {

            var retorno = await ctx.Categorias.ToListAsync();
            return retorno;

        }

        [HttpGet]
        [Route("categoria/{id}")]
        public async Task<ActionResult<Categoria>> FyndById([FromRoute] int id)
        {

            var categoria = await ctx.Categorias.AsNoTracking().Where(x => x.CategoriaId == id).Include(y => y.Produtos).FirstOrDefaultAsync();


            return categoria;
        }


        /// <summary>
        /// Realiza o cadastro de uma nova categoria
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo de request
        /// 
        /// POST Categorias/cadastrar
        /// {
        ///     
        ///     "CategoriaId":1,
        ///     "ImageUrl":"https://images.com.br",
        ///     "Nome":"Nome da categoria"
        /// 
        /// }
        /// </remarks>
        /// <param name="categoria">Objeto Json categoria</param>
        /// <returns>HttpResponse</returns>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<HttpResponse> CadastrarCategoria([FromBody] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                ctx.Categorias.Add(categoria);

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
        [Route("AlterarCategoria/{id}")]
        public async Task<ActionResult> AlterarCategoria(int id, [FromBody] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }

                if (!await ctx.Categorias.AnyAsync(x => x.CategoriaId == id))
                {
                    return NotFound();
                }

                ctx.Entry(categoria).State = EntityState.Modified;
                await ctx.SaveChangesAsync();

                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }




        [HttpDelete]
        [Route("DeletarCategoria/{id}")]
        public async Task<ActionResult> DeletarCategoria([FromRoute] int id)
        {
            var categoria = await ctx.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria != null)
            {
                ctx.Categorias.Remove(categoria);
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