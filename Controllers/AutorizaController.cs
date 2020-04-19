using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPICategoriasProdutos.DTOs;

namespace WebAPICategoriasProdutos.Controllers
{
    [Route("UserRegister")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AutorizaController(UserManager<IdentityUser> user, SignInManager<IdentityUser> sign, IConfiguration config)
        {
            userManager = user;
            signInManager = sign;
            configuration = config;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em :" + DateTime.Now.ToLongDateString();
        }

        [HttpPost]
        [Route("Join")]
        public async Task<ActionResult> RegisterUser([FromBody] UsuarioDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }


            var user = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                await signInManager.SignInAsync(user, false);
                return Ok("Cadastro realizado com sucesso");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest("Não foi possíve realizar login no sistema. Favor tente novamente.");
            }
            else
            {
                var token = GeraToken(user);

                return Ok(token);
            }

        }

        private UsuarioToken GeraToken(UsuarioDTO user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Email),
                new Claim(JwtRegisteredClaimNames.Sid,user.Password),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            //Gerando credencias utilizando a chave secreta do arquivo appsettings.json com o Algoritimo HMacSha256 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));


            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["TokenConfiguration:Issuer"],
                audience: configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires:expiration,
                signingCredentials:credentials
                );

            return new UsuarioToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Authenticated = true,
                Expiration = expiration,
                Message = "Token JWT"
            };

        }



    }
}