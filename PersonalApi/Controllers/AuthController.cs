using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Modelos;
using Modelos.NoDatabase;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilitarios;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace PersonalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class AuthController : ControllerBase
    {
        UsuarioLogica userLogica = new UsuarioLogica();

        [HttpGet]
        public IActionResult get()
        {
            return Ok("servicio en escucha");
        }
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpPost]
        public IActionResult post([FromBody] LoginRequest request)
        {
            try
            {
                // 1. Validación básica de entrada
                if (request == null || string.IsNullOrEmpty(request.Usuario) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest("Usuario y/o password incorrecto");
                }

                LoginResponse response = new LoginResponse();

                // 2. Buscamos al usuario
                UsuarioModel user = userLogica.ObtenerUsuarioPorUserName(request.Usuario);

                // 3. Verificamos si 'user' es nulo ANTES de comparar sus propiedades
                // Si es nulo o la contraseña no coincide, devolvemos el mismo error
                if (user == null || user.Correo_Electronico != request.Usuario || user.Password != UtilSecurity.encriptar_AES(request.Password))
                {
                    return BadRequest("Usuario y/o password incorrecto");
                }

                // 4. Si todo está bien, generamos el token
                user.Password = "";
                string token = generateToken(user, 59);
                response.token = token;
                response.usuario = user;

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Es mejor devolver un 500 o un mensaje controlado que un Ok() vacío en caso de error real
                return StatusCode(500, "Ocurrió un error interno en el servidor");
            }
        }


        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        //[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        //[HttpPost("RefreshToken")]
        //public IActionResult refresh([FromBody] LoginRequest request)
        //{
        //    try
        //    {
        //        string refreshToken = "";
        //        LoginResponse response = new LoginResponse();
        //        UsuarioModel user = userLogica.ObtenerUsuarioPorUserrName(request.Usuario);

        //        if (
        //            !( // "!" significa negación
        //            user.Usuario == request.Usuario
        //            && user.Password == UtilSecurity.encriptar_AES(request.Password))
        //            )
        //        {
        //            return BadRequest("Usuario y/o password incorrecto");
        //        }

        //        user.Password = "";
        //        string token = generateToken(user, 1);
        //        response.token = token;
        //        response.refreshtoken = refreshToken;
        //        response.usuario = user;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok();
        //    }
        //}




        private string generateToken(UsuarioModel user,int expireInMinutes)
        {
            //create claims details based on the user information
            IConfigurationBuilder configurationBuild = new ConfigurationBuilder();
            configurationBuild = configurationBuild.AddJsonFile("appsettings.json");
            IConfiguration configurationFile = configurationBuild.Build();
            // Leemos el archivo de configuración.
            var claims = new[] {
                       new Claim(JwtRegisteredClaimNames.Sub, configurationFile["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.ID_Usuario.ToString()),
                        new Claim("UserName", user.Correo_Electronico),
                        new Claim(ClaimTypes.Role, user.Rol),
                        new Claim("DisplayName", user.Nombre),
                        new Claim("UserName", user.Apellido),
                        new Claim("Email", user.Correo_Electronico)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationFile["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  
            var token = new JwtSecurityToken(
                configurationFile["Jwt:Issuer"],
                configurationFile["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expireInMinutes),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        } 


    }
}
