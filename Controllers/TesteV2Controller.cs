using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICategoriasProdutos.Controllers
{
    [Route("api/TesteVersaoV2")]
    [ApiController]
    [ApiVersion("2.0")]
    public class TesteV2Controller : ControllerBase
    {
        [HttpGet]
        [Route("teste")]
        public ActionResult Swagger()
        {
            return Ok("Api version 2.0");
        }

    }
}