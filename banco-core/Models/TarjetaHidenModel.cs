using banco_core.Utilities;
using Microsoft.Data.SqlClient;

namespace banco_core.Models
{
    public class TarjetaHidenModel
    {
        public int? Id { get; set; }
        public int? Cuenta_id { get; set; }
        public string? Numero_cuenta { get; set; }
        public string? Numero_tarjeta { get; set; }
        public string? Tipo_tarjeta { get; set; }
        public string? Estado_tarjeta { get; set; }
        public decimal? Saldo { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }



        public static TarjetaHidenModel MapToModel(SqlDataReader reader)
        {

            return new TarjetaHidenModel()
            {
                Id = reader.GetValueOrDefault<int?>("id"),
                Cuenta_id = reader.GetValueOrDefault<int?>("cuenta_id"),
                Numero_cuenta = reader.GetValueOrDefault<string?>("numero_cuenta"),
                Numero_tarjeta = reader.GetValueOrDefault<string?>("numero_tarjeta"),
                Tipo_tarjeta = reader.GetValueOrDefault<string?>("tipo_tarjeta"),
                Estado_tarjeta = reader.GetValueOrDefault<string?>("estado_tarjeta"),
                Saldo = reader.GetValueOrDefault<decimal?>("saldo"),
                Nombre= reader.GetValueOrDefault<string>("nombre"),
                Apellido = reader.GetValueOrDefault<string>("apellido"),

            };


        }
    }
}
