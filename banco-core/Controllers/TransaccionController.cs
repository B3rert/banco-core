using banco_core.Models;
using banco_core.Procedures;
using banco_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;

namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController(IConfiguration configuration, BancoContext context) : ControllerBase
    {

        private readonly Sp_ObtenerTransaccionesMes _Sp_ObtenerTransaccionesMes = new(configuration);

        [HttpGet()]
        public async Task<IActionResult> ObtenerTransaccionesPorIdCuentaYFecha(
            [FromHeader] int idCuenta,
            [FromHeader]  int month, 
            [FromHeader] int year
            )
        {
            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerTransaccionesMes.SpExcecute(
                account: idCuenta,
                month: month,
                year: year
                );


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }
    }
}
