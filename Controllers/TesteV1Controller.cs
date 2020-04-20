using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICategoriasProdutos.Controllers
{
    [Route("api/TesteVersaoV1")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TesteV1Controller : ControllerBase
    {
        [HttpGet]
        [Route("teste")]
        public ActionResult Swagger()
        {
            return Ok("Api Version 1.0");
        }
    }
}