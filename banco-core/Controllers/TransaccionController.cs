﻿using banco_core.Models;
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
        private readonly Sp_InsertarTransaccion _Sp_InsertarTransaccion = new(configuration);
        private readonly Sp_ObtenerTransaccionesPorRangoDeFechas _Sp_ObtenerTransaccionesPorRangoDeFechas = new(configuration);
        private readonly Sp_ObtenerTransaccionesPorAutorizacionYCredito Sp_ObtenerTransaccionesPorAutorizacionYCredito = new(configuration);


        [HttpGet("{autorizacion}")]
        public async Task<IActionResult> ValidarTransaccion(
            string autorizacion
         )
        {
            //Consumo del procedimiento
            RespondeModel response = await Sp_ObtenerTransaccionesPorAutorizacionYCredito.SpExcecute(
                autorizacion
                );


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }


        [HttpGet("rango")]
        public async Task<IActionResult> ObtenerTransaccionesPorIdCuentaYRangoFecha(
            [FromHeader] int idCuenta,
            [FromHeader] DateTime incio,
            [FromHeader] DateTime fin
            )
        {
            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerTransaccionesPorRangoDeFechas.SpExcecute(
                account: idCuenta,
                incio: incio,
                fin: fin
                );


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }


        [HttpGet("Tipo")]
        public async Task<IActionResult> ObtenerTipoTransaccion()
        {
            try
            {
                var tipos = await context.Tipo_Transaccion.ToListAsync();
                if (tipos == null || tipos.Count == 0)
                {
                    return Ok(new RespondeModel()
                    {
                        Data = new List<TipoTransaccionModel>(),
                        Success = true,
                    });
                }

                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data = tipos,
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

        [HttpPost()]
        public async Task<IActionResult> NuevaTransaccion(
            [FromBody] NewTraModel tra
            )
        {
            //Consumo del procedimiento
            RespondeModel response = await _Sp_InsertarTransaccion.SpExcecute(
               tra
                );


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }

        [HttpGet()]
        public async Task<IActionResult> ObtenerTransaccionesPorIdCuentaYFecha(
            [FromHeader] int idCuenta,
            [FromHeader] int month,
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
