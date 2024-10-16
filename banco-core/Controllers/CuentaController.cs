using banco_core.Models;
using banco_core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace banco_core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController(BancoContext context) : ControllerBase
    {
        private readonly BancoContext _context = context;

    [HttpPost()]
    public async Task<IActionResult> CrearCuenta([FromBody] CuentaModel cuenta)
        {
            // Verifica si ya existe una cuenta con el mismo número de cuenta
            if (await _context.Cuenta.AnyAsync(c => c.Numero_cuenta == cuenta.Numero_cuenta))
            {
                return Ok(new RespondeModel()
                {
                    Success = false,
                    Data    = "El número de cuenta ya está en uso.",
                });
            }

           
            // Agrega la nueva cuenta al contexto
            _context.Cuenta.Add(cuenta);
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

            return CreatedAtAction(nameof(CrearCuenta), new RespondeModel()
            {
                Data  = cuenta,
                Success = true,
            });
        }

        // Endpoint para obtener las cuentas de un cliente específico
        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ObtenerCuentasPorCliente(int clienteId)
        {
            try
            {
                // Obtiene las cuentas del cliente con el clienteId
                var cuentas = await _context.Cuenta
                    .Where(c => c.Cliente_id == clienteId)
                    .ToListAsync();

                // Si no se encuentran cuentas, devuelve un error 404
                if (cuentas == null || cuentas.Count == 0)
                {
                    return Ok( new RespondeModel()
                    {
                        Data = new List<CuentaModel>(),
                        Success = true,
                    });
                }

                return Ok(new RespondeModel()
                {
                    Data = cuentas,
                    Success = true,
                }); // Devuelve las cuentas encontradas
            }
            catch (Exception e)
            {

                return BadRequest(new RespondeModel()
                {
                    Success = false,
                    Data = e.Message,
                });
            }
        }
    }
}
