﻿using banco_core.Models;
using banco_core.Procedures;
using banco_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController(IConfiguration configuration, BancoContext context) : ControllerBase
    {
        private readonly Sp_InsertarTarjeta _Sp_InsertarTarjeta = new(configuration);


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