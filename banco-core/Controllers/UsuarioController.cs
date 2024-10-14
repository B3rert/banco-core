using banco_core.Models;
using banco_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BancoContext _context;

        public UsuarioController(BancoContext context)
        {
            _context = context;
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

            // Si todo es correcto, puedes devolver el usuario o un token de autenticación
            return Ok(new { message = "Login exitoso", usuario.Usuario, usuario.Rol_Id});
        }
    }
}
