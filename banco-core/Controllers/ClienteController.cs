using banco_core.Models;
using banco_core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController(BancoContext context) : ControllerBase
    {

        [HttpGet("{dpi}")]
        public async Task<IActionResult> ObtenerClientePorDpi(string dpi)
        {
            
            try
            {
                // Buscar al cliente por su DPI
                var cliente = await context.Cliente
                    .FirstOrDefaultAsync(c => c.Dpi == dpi);

                if (cliente == null)
                {
                    return Ok(new RespondeModel()
                    {
                        Data = "Cliente no encontrado.",
                        Success = false,
                    });
                }

                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data    = cliente,
                }); // Devuelve el cliente si se encuentra
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


        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteModel cliente)
        {
            try
            {

                if (cliente == null)
                {
                    return BadRequest(new RespondeModel()
                    {
                        Success = false,
                        Data = "El cliente no puede ser nulo.",
                    });
                }

                // Verifica si el DPI ya existe en la base de datos
                var dpiExistente = await context.Cliente
                    .AnyAsync(c => c.Dpi == cliente.Dpi);

                if (dpiExistente)
                {
                    return BadRequest(new RespondeModel()
                    {
                        Data = "El DPI ya existe.",
                        Success = false,
                    });
                }

                // Agrega el nuevo cliente al contexto
                context.Cliente.Add(cliente);

                // Guarda los cambios en la base de datos
                await context.SaveChangesAsync();

                // Devuelve la respuesta creada con la ubicación del nuevo cliente
                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data = cliente,
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
