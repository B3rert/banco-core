using banco_core.Models;
using banco_core.Utilities;
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
    public class UsuarioController : ControllerBase
    {
        private readonly BancoContext _context;
        private readonly IConfiguration _config;


        public UsuarioController(BancoContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginRequest)
        {
            // Busca al usuario por su nombre de usuario
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Usuario == loginRequest.User || u.Correo == loginRequest.User);

            // Verifica si el usuario existe y si la contraseña es correcta
            if (usuario == null || usuario.Clave != loginRequest.Password)
            {
                // Si el usuario no existe o la contraseña es incorrecta, retorna 401 Unauthorized
                return Unauthorized(new { message = "Credenciales incorrectas" });
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

            // Si todo es correcto, puedes devolver el usuario o un token de autenticación
            return Ok(new { message = "Login exitoso", usuario.Usuario, usuario.Rol_Id});
        }
    }
}
