using banco_core.Utilities;
using Microsoft.Data.SqlClient;

namespace banco_core.Models
{
    public class CuentaModel
    {
        
            public int? Id { get; set; }
            public string? Numero_cuenta { get; set; }
            public decimal? Saldo { get; set; }
            public DateTime Fecha_apertura { get; set; }
            public int Cliente_id { get; set; }
            public int Tipo_cuenta_id { get; set; }

        public static CuentaModel MapToModel(SqlDataReader reader)
        {

            return new CuentaModel()
            {

                Id = reader.GetValueOrDefault<int>("Id"),
                Numero_cuenta = reader.GetValueOrDefault<string>("Numero_cuenta"),
                Saldo = reader.GetValueOrDefault<decimal>("Saldo"),
                Fecha_apertura = reader.GetValueOrDefault<DateTime>("Fecha_apertura"),
                Cliente_id = reader.GetValueOrDefault<int>("Cliente_id"),
                Tipo_cuenta_id = reader.GetValueOrDefault<int>("Tipo_cuenta_id"),
            };


        }

    }
}
