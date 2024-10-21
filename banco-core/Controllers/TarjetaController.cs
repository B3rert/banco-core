using banco_core.Modelo;
using banco_core.Models;
using banco_core.Procedures;
using banco_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController(IConfiguration configuration, BancoContext context) : ControllerBase
    {
        private readonly Sp_InsertarTarjeta _Sp_InsertarTarjeta = new(configuration);
        private readonly Sp_ObtenerTarjetasPorUsuario _Sp_ObtenerTarjetasPorUsuario = new(configuration);
        private readonly Sp_ObtenerTarjetaPorId _Sp_ObtenerTarjetaPorId = new(configuration);

        [HttpPost("estado")]
        public async Task<IActionResult> ActualizarEstado([FromBody] CardStatusModel status)
        {
            try
            {
                // Busca la tarjeta por su ID
                var tarjeta = await context.Tarjeta.FindAsync(status.id);
                if (tarjeta == null)
                {
                    return NotFound(new RespondeModel()
                    {
                        Data = "Tarjeta no encontrada.",
                        Success = false
                    });
                }

                // Actualiza el campo Estado
                tarjeta.Estado_id = status.estado;

                // Guarda los cambios en la base de datos
                await context.SaveChangesAsync();

                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data    = "Estado de la tarjeta actualizado exitosamente."
                });
            }
            catch (Exception e)
            {
                return  BadRequest(new RespondeModel()
                {
                    Data= e.Message,
                    Success = false,
                    
                });
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObtnerTarjetaPorUsuario(int id)
        {

            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerTarjetaPorId.SpExcecute(id);



            //respuesta correcta 200
            if (response.Success)
            {

                List<TarjetaShowModel>? tarjetas = response.Data as List<TarjetaShowModel>;

                response.Data = tarjetas![0];

                return Ok(response);

            };

            //respuest aincorrecta 400
            return BadRequest(response);

        }
        [HttpGet("usuario/{usuario}")]
        public async Task<IActionResult> ObtnerTarjetaPorUsuario(string usuario)
        {

            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerTarjetasPorUsuario.SpExcecute(usuario);


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }

        [HttpPost()]
        public async Task<IActionResult> CrearTarjeta([FromBody] TarjetaModel tarjeta)
        {
            //Consumo del procedimiento
            RespondeModel response = await _Sp_InsertarTarjeta.SpExcecute(tarjeta);


            //respuesta correcta 200
            if (response.Success)
            {

                List<TarjetaModel>? tarjetas = response.Data as List<TarjetaModel>;

                response.Data = tarjetas![0];

                return Ok(response);

            };

            //respuest aincorrecta 400
            return BadRequest(response);
        }
    }
}
