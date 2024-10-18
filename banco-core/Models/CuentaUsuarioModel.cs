using banco_core.Utilities;
using Microsoft.Data.SqlClient;


namespace banco_core.Models
{
    public class CuentaUsuarioModel
    {
        public int? Id { get; set; }
        public string? Numero_cuenta { get; set; }
        public decimal? Saldo { get; set; }
        public string? Tipo_cuenta { get; set; }

        public static CuentaUsuarioModel MapToModel(SqlDataReader reader)
        {

            return new CuentaUsuarioModel()
            {
                Id = reader.GetValueOrDefault<int>("Id"),
                Numero_cuenta = reader.GetValueOrDefault<string>("Numero_cuenta"),
                Saldo = reader.GetValueOrDefault<decimal>("Saldo"),
                Tipo_cuenta = reader.GetValueOrDefault<string>("Tipo_cuenta"),
            };


        }

    }
}
