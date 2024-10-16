using banco_core.Models;
using banco_core.Procedures;
using banco_core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController(IConfiguration configuration, BancoContext context) : ControllerBase
    {

        //Servicion con el consumo de los procedimientos
        private readonly Sp_InsertarCuenta _Sp_InsertarCuenta = new(configuration);

        [HttpPost()]
        public async Task<IActionResult> CrearCuenta([FromBody] CuentaModel cuenta)
        {
            //Consumo del procedimiento
            RespondeModel response = await _Sp_InsertarCuenta.SpExcecute(cuenta);


            //respuesta correcta 200
            if (response.Success) {

                List<CuentaModel>? cuentas = response.Data as List<CuentaModel>;

                response.Data = cuentas![0];

                return Ok(response);

            };

            //respuest aincorrecta 400
            return BadRequest(response);
        }

        // Endpoint para obtener las cuentas de un cliente específico
        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ObtenerCuentasPorCliente(int clienteId)
        {
            try
            {
                // Obtiene las cuentas del cliente con el clienteId
                var cuentas = await context.Cuenta
                    .Where(c => c.Cliente_id == clienteId)
                    .ToListAsync();

                // Si no se encuentran cuentas, devuelve un error 404
                if (cuentas == null || cuentas.Count == 0)
                {
                    return Ok(new RespondeModel()
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
