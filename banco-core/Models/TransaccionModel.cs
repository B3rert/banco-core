using banco_core.Modelo;
using Microsoft.Data.SqlClient;
using banco_core.Utilities;

namespace banco_core.Models
{
    public class TransaccionModel
    {
        public int? Id { get; set; }
        public int? Cuenta_id { get; set; }
        public int? Tipo_transaccion_id { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Descripcion { get; set; }
        public int? Realizado_por { get; set; }
        public string? Numero_autorizacion { get; set; }

        public static TransaccionModel MapToModel(SqlDataReader reader)
        {

            return new TransaccionModel()
            {
                Id = reader.GetValueOrDefault<int>("Id"),
                Cuenta_id = reader.GetValueOrDefault<int>("Cuenta_id"),
                Tipo_transaccion_id = reader.GetValueOrDefault<int>("Tipo_transaccion_id"),
                Monto = reader.GetValueOrDefault<decimal>("Monto"),
                Fecha = reader.GetValueOrDefault<DateTime>("Fecha"),
                Descripcion = reader.GetValueOrDefault<string>("Descripcion"),
                Numero_autorizacion = reader.GetValueOrDefault<string>("numero_autorizacion"),
                Realizado_por = reader.GetValueOrDefault<int>("Realizado_por"),
            };

        }






    }
}
