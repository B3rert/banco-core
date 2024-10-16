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

    }
}
