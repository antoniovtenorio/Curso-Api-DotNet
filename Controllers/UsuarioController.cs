using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Permite Autenticar o usuario no sistema
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>retorna status ok, dadis di usuario e token de acesso.</returns>
        /// 
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar usuario", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios devem ser preenchidos", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult logar(LoginViewModelInput loginViewModelInput)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(new ValidaCampoViewModelOutput(ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage)));
            }*/
            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = 1,
                Login = "victortenorio",
                Email = "victortenorio@gmail.com"

            };
            var secret = Encoding.ASCII.GetBytes("Mjhsdjj$#Gsdff()rfGcvb#$5421258766533GVVBCasaadR43*(¨%$");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new { 
            Token = token,
            Usuario = usuarioViewModelOutput
            });




            return Ok(loginViewModelInput);
        }

        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput loginViewModelInput)
        {
            return Created("", loginViewModelInput);
        }
    }
}
