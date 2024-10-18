using Microsoft.Data.SqlClient;
using banco_core.Utilities;


namespace banco_core.Models
{
    public class TransaccionMesModel
    {
        public decimal? Monto { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Tipo_transaccion { get; set; }
        public bool? Es_credito { get; set; }

        public static TransaccionMesModel MapToModel(SqlDataReader reader)
        {

            return new TransaccionMesModel()
            {

                Monto = reader.GetValueOrDefault<decimal>("Monto"),
                Fecha = reader.GetValueOrDefault<DateTime>("Fecha"),
                Tipo_transaccion = reader.GetValueOrDefault<string>("Tipo_transaccion"),
                Es_credito = reader.GetValueOrDefault<bool>("Es_credito"),
            };


        }
    }
}
