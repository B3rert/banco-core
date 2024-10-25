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
        private readonly Sp_InsertarTransaccion _Sp_InsertarTransaccion = new(configuration);


        [HttpPost("pago")]
        public async Task<IActionResult> RealizarPago([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                // 1. Validar que la tarjeta exista y el CVV sea correcto
                var card = await context.Tarjeta.FirstOrDefaultAsync(t =>
                    t.Numero_tarjeta == paymentRequest.CardNumber &&
                    t.Cvv == int.Parse(paymentRequest.CVV!) &&
                     t.Fecha_vencimiento.HasValue &&
                    t.Fecha_vencimiento.Value.Date == paymentRequest.ExpirationDate.Date);

                if (card == null)
                {
                    return NotFound(
                        new RespondeModel()
                        {
                            Success = false,
                            Data = "Tarjeta invalida.",
                        }
                        );
                }

                // 2. Validar que la tarjeta esté activa
                if (card.Estado_id != 1)
                {
                    return BadRequest(
                        new RespondeModel()
                        {
                            Success = false,
                            Data = "Tarjeta inactiva."
                        }
                        );
                }

                // 3. Validar que la tarjeta no haya expirado
                if (card.Fecha_vencimiento < DateTime.Now)
                {
                    return BadRequest(new RespondeModel()
                    {
                        Data = "Tarjeta expirada.",
                        Success = false,
                    });
                }

                // 4. Validar que la cuenta asociada a la tarjeta tenga fondos suficientes
                var account = await context.Cuenta.FirstOrDefaultAsync(c => c.Id == card.Cuenta_id);

                if (account == null || account.Saldo < paymentRequest.Amount)
                {
                    return BadRequest(new RespondeModel()
                    {
                        Data = "Fondos insuficientes.",
                        Success = false,
                    });
                }

                // 5. Validar que la cuenta destino existe
                var destinationAccount = await context.Cuenta.FirstOrDefaultAsync(c =>
                    c.Numero_cuenta == paymentRequest.DestinationAccount);

                if (destinationAccount == null)
                {
                    return BadRequest(new RespondeModel()
                    {
                        Success = false,
                        Data = "Cuenta destino inexistente.",
                    });
                }

                // 6. Preparar el objeto `tra` para la transacción
                var debitTra = new NewTraModel
                {
                    Desc = "Pago Grandes Genios",
                    CuentaId = card.Cuenta_id,
                    Monto = paymentRequest.Amount,
                    TipoTra = 11, //Pago
                    UserId = 23
                };

                var creditTra = new NewTraModel
                {
                    UserId = 23,
                    TipoTra = 12, //cobro
                    Monto = paymentRequest.Amount,
                    CuentaId = destinationAccount.Id,
                    Desc = "ATM Grandes Genios"
                };


                // 7. Ejecutar el procedimiento almacenado para realizar la transacción
                var responseDebit = await _Sp_InsertarTransaccion.SpExcecute(tra: debitTra);

                if (!responseDebit.Success)
                {
                    return BadRequest(new RespondeModel()
                    {
                        Success = false,
                        Data = "Tarjeta rechazada."
                    });
                }


                var responseCredit = await _Sp_InsertarTransaccion.SpExcecute(tra: creditTra);

                //TODO:Esto es un problema, no se controlan errores, ya que el cobro se hizo pero el pago no.
                if (!responseDebit.Success)
                {
                    return BadRequest(new RespondeModel()
                    {
                        Success = false,
                        Data = "Tarjeta rechazada."
                    });
                }

                return Ok(responseDebit);
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
                    Data = "Estado de la tarjeta actualizado exitosamente."
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
        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> ObtnerTarjetaPorUsuarioHiden(int id)
        {

            //Consumo del procedimiento
            RespondeModel response = await _Sp_ObtenerTarjetasPorUsuario.SpExcecute(id);


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
