using Microsoft.Data.SqlClient;
using banco_core.Utilities;


namespace banco_core.Models
{
    public class TarjetaShowModel
    {
        public string? Numero_tarjeta { get; set; }
        public string? Fecha_vencimiento { get; set; }
        public int? Cvv { get; set; }

        public static TarjetaShowModel MapToModel(SqlDataReader reader)
        {

            return new TarjetaShowModel()
            {

                Numero_tarjeta = reader.GetValueOrDefault<string>("Numero_tarjeta"),
                Fecha_vencimiento = reader.GetValueOrDefault<string>("Fecha_vencimiento"),
                Cvv = reader.GetValueOrDefault<int>("Cvv"),

            };


        }
    }
}
