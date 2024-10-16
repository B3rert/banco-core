using banco_core.Modelo;
using banco_core.Models;
using banco_core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(BancoContext context, IConfiguration configuration) : ControllerBase
    {
        private readonly BancoContext _context = context;
        private readonly IConfiguration _config = configuration;


        [HttpPost("crear")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioModel user)
        {
            try
            {
                // Verifica si el nombre de usuario ya está en uso
                if (await _context.Usuario.AnyAsync(u => u.Usuario == user.Usuario || u.Correo == user.Correo))
                {
                    return Ok(new RespondeModel()
                    {
                        Data = "El nombre de usuario ya está en uso.",
                        Success = false,
                    });
                }

                // Agrega el nuevo usuario al contexto
                _context.Usuario.Add(user);
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data = user,
                });
            }
            catch (Exception e)
            {

                return BadRequest(new RespondeModel()
                {
                    Data=e.Message,
                    Success = false,
                });

            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginRequest)
        {
            try
            {
                // Busca al usuario por su nombre de usuario
                var usuario = await _context.Usuario
                    .FirstOrDefaultAsync(u => u.Usuario == loginRequest.User || u.Correo == loginRequest.User);

                // Verifica si el usuario existe y si la contraseña es correcta
                if (usuario == null || usuario.Clave != loginRequest.Password)
                {
                    // Si el usuario no existe o la contraseña es incorrecta, retorna 401 Unauthorized
                    return Ok(new RespondeModel()
                    {
                        Data = "Credenciales incorrectas",
                        Success = false
                    }); ;
                }



                //Crear llave secreta para token
                var secretKey = _config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                //Agregar contenido token
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginRequest.User!));

                //Configurar token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddDays(360),

                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                //Crear token
                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                var barerToken = tokenHandler.WriteToken(createdToken);

                usuario.Clave = barerToken;

                // Si todo es correcto, puedes devolver el usuario o un token de autenticación
                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data = usuario,
                });
            }
            catch (Exception e)
            {

                return BadRequest(new RespondeModel()
                {
                    Data= e.Message,
                    Success = false,
                });
            }
        }
    }
}
