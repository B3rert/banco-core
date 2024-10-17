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
        public async Task<IActionResult> CrearUsuario([FromBody] NewUserModel user)
        {
            try
            {
                // Validar si los campos de nombres y apellidos no están vacíos
                if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Apellido))
                {
                    return BadRequest(new RespondeModel()
                    {
                        Data = "El nombre y el apellido son obligatorios.",
                        Success = false,
                    });
                }

                // Generar un nombre de usuario único
                string nuevoUsuario = await GenerarNombreDeUsuario(user.Nombre, user.Apellido);

                // Generar una contraseña segura
                string nuevaContrasena = GenerarContrasenaSegura();


                UsuarioModel usuario = new()
                {
                    Clave = nuevaContrasena,
                    Rol_Id = user.Rol,
                    Usuario = nuevoUsuario,
                };


                // Agregar el nuevo usuario al contexto
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data = usuario, // Devolver usuario y contraseña generados
                });
            }
            catch (Exception e)
            {
                return BadRequest(new RespondeModel()
                {
                    Data = e.Message,
                    Success = false,
                });
            }
        }

        // Método para generar nombre de usuario
        private async Task<string> GenerarNombreDeUsuario(string nombre, string apellido)
        {
            string usuarioBase = nombre.Substring(0, 1).ToLower() + apellido.ToLower(); // Ejemplo: jperez
            string nuevoUsuario = usuarioBase;
            int contador = 1;

            // Verificar si el usuario ya existe y seguir incrementando el número si es necesario
            while (await _context.Usuario.AnyAsync(u => u.Usuario == nuevoUsuario))
            {
                nuevoUsuario = usuarioBase + contador.ToString(); // Ejemplo: jperez1, jperez2
                contador++;
            }

            return nuevoUsuario;
        }

        // Método para generar una contraseña segura
        private string GenerarContrasenaSegura()
        {
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
            Random random = new Random();
            return new string(Enumerable.Repeat(caracteres, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginRequest)
        {
            try
            {
                // Busca al usuario por su nombre de usuario
                var usuario = await _context.Usuario
                    .FirstOrDefaultAsync(u => u.Usuario == loginRequest.User);

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
                    Data = e.Message,
                    Success = false,
                });
            }
        }
    }
}
