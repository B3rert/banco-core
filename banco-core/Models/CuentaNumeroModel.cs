using Microsoft.Data.SqlClient;
using banco_core.Utilities;

namespace banco_core.Models
{
    public class CuentaNumeroModel
    {
        public int? Id { get; set; }
        public string? Numero_cuenta { get; set; }
        public string? Tipo_cuenta { get; set; }
        public string? Nombre_completo { get; set; }

        public static CuentaNumeroModel MapToModel(SqlDataReader reader)
        {

            return new CuentaNumeroModel()
            {
                Id = reader.GetValueOrDefault<int?>("id"),
                Numero_cuenta = reader.GetValueOrDefault<string?>("numero_cuenta"),
                Tipo_cuenta = reader.GetValueOrDefault<string?>("tipo_cuenta"),
                Nombre_completo = reader.GetValueOrDefault<string?>("nombre_completo"),


            };


        }
    }
}
