using banco_core.Utilities;
using Microsoft.Data.SqlClient;

namespace banco_core.Models
{
    public class CuentaNumeroDpiModel
    {
        public int? Id { get; set; }
        public string? Numero_cuenta { get; set; }
        public string? Tipo_cuenta { get; set; }
        public decimal? Saldo { get; set; }
        public string? Nombre_completo { get; set; }

        public static CuentaNumeroDpiModel MapToModel(SqlDataReader reader)
        {

            return new CuentaNumeroDpiModel()
            {
                Id = reader.GetValueOrDefault<int?>("id"),
                Numero_cuenta = reader.GetValueOrDefault<string?>("numero_cuenta"),
                Tipo_cuenta = reader.GetValueOrDefault<string?>("tipo_cuenta"),
                Saldo = reader.GetValueOrDefault<decimal?>("Saldo"),
                Nombre_completo = reader.GetValueOrDefault<string?>("nombre_completo"),


            };


        }
    }
}
