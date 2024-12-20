﻿using banco_core.Models;
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
        private readonly Sp_ObtenerCuentasPorUsuario _SP_ObtenerCuentasPorUsuario = new(configuration);
        private readonly Sp_ObtenerCuentaPorNumero _Sp_ObtenerCuentaPorNumero = new(configuration);
        private readonly Sp_ObtenerCuentaPorNumeroDPI _Sp_ObtenerCuentaPorNumeroDPI = new(configuration);
        private readonly Sp_ObtenerCuentaDpi _Sp_ObtenerCuentaDpi = new(configuration);


        [HttpGet("dpi/{dpi}")]
        public async Task<IActionResult> ObtenerCuentasPorDpi (string dpi)
        {

            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerCuentaDpi.SpExcecute(
                dpi
            );


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }


        [HttpGet("dpi/{cuenta}/{dpi}")]
        public async Task<IActionResult> ObtenerCuentasPorNumeroDpi(string cuenta, string dpi)
        {

            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerCuentaPorNumeroDPI.SpExcecute(
                cuenta,
                dpi
                );


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }

        [HttpGet("numero/{cuenta}")]
        public async Task<IActionResult> ObtenerCuentasPorNumero(string cuenta)
        {

            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerCuentaPorNumero.SpExcecute(cuenta);


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }


        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> ObtenerCuentasPorUsuario(int id)
        {

            //Consumo del procedimiento
            RespondeModel response = await _SP_ObtenerCuentasPorUsuario.SpExcecute(id);


            //respuesta correcta 200
            if (response.Success)

                return Ok(response);

            //respuest aincorrecta 400
            return BadRequest(response);
        }

        [HttpGet("tipo")]
        public async Task<IActionResult> ObtenerTiposDeCuenta()
        {
            try
            {
                // Obtiene todos los tipos de cuenta de la base de datos
                var tiposCuenta = await context.Tipo_Cuenta.ToListAsync();

                // Verifica si la lista está vacía
                if (tiposCuenta == null || tiposCuenta.Count == 0)
                {
                    return Ok(new RespondeModel()
                    {
                        Data = new List<TipoCuentaModel>(),
                        Success = true,
                    });
                }

                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data = tiposCuenta,
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
        public async Task<IActionResult> CrearCuenta([FromBody] CuentaModel cuenta)
        {
            //Consumo del procedimiento
            RespondeModel response = await _Sp_InsertarCuenta.SpExcecute(cuenta);


            //respuesta correcta 200
            if (response.Success)
            {

                List<CuentaModel>? cuentas = response.Data as List<CuentaModel>;

                response.Data = cuentas![0];

                return Ok(response);

            };

            //respuest aincorrecta 400
            return BadRequest(response);
        }


    }
}
