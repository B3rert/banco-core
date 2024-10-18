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

    }
}
