using Microsoft.Data.SqlClient;
using banco_core.Utilities;


namespace banco_core.Models
{
    public class TarjetaModel
    {
        
            public int? Id { get; set; }
            public int? Cuenta_id { get; set; }
            public int? Tipo_tarjeta_id { get; set; }
            public string? Numero_tarjeta { get; set; }
            public DateTime? Fecha_emision { get; set; }
            public DateTime? Fecha_vencimiento { get; set; }
            public int? Cvv { get; set; }
            public int? Estado_id { get; set; }



        public static TarjetaModel MapToModel(SqlDataReader reader)
        {

            return new TarjetaModel()
            {

                Id = reader.GetValueOrDefault<int>("Id"),
                Cuenta_id = reader.GetValueOrDefault<int>("Cuenta_id"),
                Tipo_tarjeta_id = reader.GetValueOrDefault<int>("Tipo_tarjeta_id"),
                Numero_tarjeta = reader.GetValueOrDefault<string>("Numero_tarjeta"),
                Fecha_emision = reader.GetValueOrDefault<DateTime>("Fecha_emision"),
                Fecha_vencimiento = reader.GetValueOrDefault<DateTime>("Fecha_vencimiento"),
                Cvv = reader.GetValueOrDefault<int>("Cvv"),
                Estado_id = reader.GetValueOrDefault<int>("Estado_id"),
            };


        }
    }
}
